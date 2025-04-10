using Task_API.Models;

namespace Task_API.Services.Interfaces
{
    public interface IPermissionServices
    {
        Task<List<Permission>> GetAll();
        Task<Permission> GetById(int id);
        Task<Permission> Add(Permission obj);
        Task<Permission> Update(Permission obj);
        Task<bool> Delete(int id);
    }
}
