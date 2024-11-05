using ApiProject.Entities.DB;
using ApiProject.Entities.Models;
using ApiProject.Infrastructure.Repository;
using ApiProject.Interfaces.Services;
using Serilog;
using System.Text.Json;

namespace ApiProject.Service
{
    public class OperationService : IOperationService
    {
        private readonly ILogger _log;
        private readonly IApiProjectRepository _repo;

        public OperationService(ILogger log, IApiProjectRepository repo)
        {
            _log = log;
            this._repo = repo;
        }

        public async Task<bool> AddOperationAsync(OperationModel item)
        {
            _log.Debug($"Creating Operation {JsonSerializer.Serialize(item)}");

            if (item.ClientId.HasValue && !_repo.Any<Client>(c => c.ClientId == item.ClientId.Value))
            {
                _log.Error($"Client with id {item.ClientId.Value} not found.");

                return false;
            }

            if (item.Value == 0)
            {
                _log.Error($"Invalid Value {item.Value}.");

                return false;
            }

            if (!_repo.Any<Product>(c => c.ProductId == item.ProductId))
            {
                _log.Error($"Product with id {item.ProductId} not found.");

                return false;
            }

            if (!_repo.Any<Seller>(c => c.SellerId == item.SellerId))
            {
                _log.Error($"Seller with id {item.SellerId} not found.");

                return false;
            }


            if (_repo.Any<Operation>(c => c.ClientId == item.ClientId && c.ProductId == item.ProductId && c.OperationDate == item.OperationDate && c.OperationType == item.OperationType))
            {
                _log.Error("Operation with same values already exists.");

                return false;
            }

            _repo.Add(new Operation
            {
                ClientId = item.ClientId,
                ProductId = item.ProductId,
                SellerId = item.SellerId,
                Value = item.Value,
                OperationType = item.OperationType,
                OperationDate = item.OperationDate
            });

            _repo.SaveChanges();

            _log.Debug("Created");

            return true;
        }

        public async Task<bool> RemoveOperationAsync(OperationModel item)
        {
            _log.Debug($"Deleting Operation {JsonSerializer.Serialize(item)}");

            var dbItem = await _repo.FirstOrDefaultAsync<Operation>(c => c.ClientId == item.ClientId && c.ProductId == item.ProductId && c.OperationDate == item.OperationDate && c.OperationType == item.OperationType);

            if (dbItem == null)
            {
                _log.Error("Operation doesn't not exists.");

                return false;
            }

            _repo.Delete(dbItem);

            _repo.SaveChanges();

            _log.Debug("Deleted");

            return true;
        }
    }
}
