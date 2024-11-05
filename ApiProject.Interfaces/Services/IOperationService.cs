using ApiProject.Entities.Models;

namespace ApiProject.Interfaces.Services
{
    public interface IOperationService
    {
        Task<bool> AddOperationAsync(OperationModel item);
        Task<bool> RemoveOperationAsync(OperationModel item);
    }
}