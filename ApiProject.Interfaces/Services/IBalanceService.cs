using ApiProject.Entities.Models;

namespace ApiProject.Interfaces.Services
{
    public interface IBalanceService
    {
        Task<bool> ConsolidateAsync();
        Task<BalanceModel> GetBalance(DateTime date);
        Task<BalanceModel> GetBalanceByClients(DateTime date);
        Task<BalanceModel> GetBalanceByProducts(DateTime date);
        Task<BalanceModel> GetBalanceBySellers(DateTime date);
    }
}