using Task_API.Models;

namespace Task_API.Services.Interfaces
{
    public interface IRolePermissionServices
    {
        Task<List<RolePermission>> GetAll();
        Task<RolePermission> GetById(int id);
        Task<RolePermission> Add(RolePermission obj);
        Task<RolePermission> Update(RolePermission obj);
        Task<bool> Delete(int id);
    }
}
