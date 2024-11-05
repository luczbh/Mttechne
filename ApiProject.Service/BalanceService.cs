using ApiProject.Entities.DB;
using ApiProject.Entities.Enumerators;
using ApiProject.Entities.Models;
using ApiProject.Infrastructure.Repository;
using ApiProject.Interfaces.Services;
using Serilog;


namespace ApiProject.Service
{
    public class BalanceService : IBalanceService
    {
        private readonly ILogger _log;
        private readonly IApiProjectRepository _repo;

        public BalanceService(ILogger log, IApiProjectRepository repo)
        {
            _log = log;
            this._repo = repo;
        }

        private void ConsolidateByClient(DateTime date, IEnumerable<Operation> operations)
        {
            var data = operations
                .GroupBy(g => g.ClientId)
                .Select(cl => new Balance
                {
                    ClientId = cl.First().ClientId,
                    Date = date,
                    TotalCredits = cl.Where(c => c.OperationType == EOperationType.Credit).Sum(o => o.Value),
                    TotalDebits = cl.Where(c => c.OperationType == EOperationType.Debit).Sum(o => o.Value)
                });

            _repo.AddRange(data);
        }

        private void ConsolidateByProduct(DateTime date, IEnumerable<Operation> operations)
        {
            var data = operations
                .GroupBy(g => g.ProductId)
                .Select(cl => new Balance
                {
                    ProductId = cl.First().ProductId,
                    Date = date,
                    TotalCredits = cl.Where(c => c.OperationType == EOperationType.Credit).Sum(o => o.Value),
                    TotalDebits = cl.Where(c => c.OperationType == EOperationType.Debit).Sum(o => o.Value)
                });

            _repo.AddRange(data);
        }

        private void ConsolidateBySeller(DateTime date, IEnumerable<Operation> operations)
        {
            _repo.AddRange(operations
                .GroupBy(g => g.SellerId)
                .Select(cl => new Balance
                {
                    SellerId = cl.First().SellerId,
                    Date = date,
                    TotalCredits = cl.Where(c => c.OperationType == EOperationType.Credit).Sum(o => o.Value),
                    TotalDebits = cl.Where(c => c.OperationType == EOperationType.Debit).Sum(o => o.Value)
                }));
        }

        public async Task<bool> ConsolidateAsync()
        {
            var consolidationDate = (await _repo.MaxDateAsync<Balance>(c => true, c => c.Date.Date)).Date;

            _log.Debug("Start consolidation");

            //For the first time we'll get yestarday as default value
            if (consolidationDate == DateTime.MinValue)
                consolidationDate = DateTime.Today.AddDays(-1);

            _log.Debug($"Last consolidation date: {consolidationDate}");

            do
            {
                _repo.DeleteWhere<Balance>(c => c.Date == consolidationDate);

                var operations = await _repo.FindByAsync<Operation>(c => c.OperationDate.Date == consolidationDate);
                ConsolidateByClient(consolidationDate, operations.Where(c => c.ClientId.HasValue));
                ConsolidateByProduct(consolidationDate, operations);
                ConsolidateBySeller(consolidationDate, operations);

                _repo.SaveChanges();

                consolidationDate = consolidationDate.AddDays(1);

                _log.Debug($"Consolidated day: {consolidationDate}");

            } while (consolidationDate <= DateTime.Today);

            return true;
        }

        public async Task<BalanceModel> GetBalance(DateTime date)
        {
            var data = await _repo.FindByAsync<Balance>(c => c.Date.Date == date.Date && c.ProductId.HasValue);

            var result = new BalanceModel
            {
                Date = date,
                Description = "Balance",
                TotalCredits = data.Sum(c => c.TotalCredits),
                TotalDebits = data.Sum(c => c.TotalDebits),
            };

            return result;
        }

        public async Task<BalanceModel> GetBalanceByClients(DateTime date)
        {
            var data = await _repo.FindByAsync<Balance>(c => c.Date.Date == date.Date && c.ClientId.HasValue, i => i.Client);

            var result = new BalanceModel
            {
                Date = date,
                Description = "Balance By Clients",
                TotalCredits = data.Sum(c => c.TotalCredits),
                TotalDebits = data.Sum(c => c.TotalDebits),
                Operations = data.Select(c => new BalanceItemModel
                {
                    Id = c.ClientId.Value,
                    Name = c.Client.Name,
                    Credits = c.TotalCredits,
                    Debits = c.TotalDebits
                })
            };

            return result;
        }

        public async Task<BalanceModel> GetBalanceByProducts(DateTime date)
        {
            var data = await _repo.FindByAsync<Balance>(c => c.Date.Date == date.Date && c.ProductId.HasValue, i => i.Product);

            var result = new BalanceModel
            {
                Date = date,
                Description = "Balance By Products",
                TotalCredits = data.Sum(c => c.TotalCredits),
                TotalDebits = data.Sum(c => c.TotalDebits),
                Operations = data.Select(c => new BalanceItemModel
                {
                    Id = c.ProductId.Value,
                    Name = c.Product.ProductName,
                    Credits = c.TotalCredits,
                    Debits = c.TotalDebits
                })
            };

            return result;
        }

        public async Task<BalanceModel> GetBalanceBySellers(DateTime date)
        {
            var data = await _repo.FindByAsync<Balance>(c => c.Date.Date == date.Date && c.SellerId.HasValue, i => i.Seller);

            var result = new BalanceModel
            {
                Date = date,
                Description = "Balance By Sellers",
                TotalCredits = data.Sum(c => c.TotalCredits),
                TotalDebits = data.Sum(c => c.TotalDebits),
                Operations = data.Select(c => new BalanceItemModel
                {
                    Id = c.SellerId.Value,
                    Name = c.Seller.SellerName,
                    Credits = c.TotalCredits,
                    Debits = c.TotalDebits
                })
            };

            return result;
        }
    }
}
