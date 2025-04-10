using Task_API.Models;

namespace Task_API.Services.Interfaces
{
    public interface IRoleServices
    {
        Task<List<Role>> GetAll();
        Task<Role> GetById(int id);
        Task<Role> Add(Role obj);
        Task<Role> Update(Role obj);
        Task<bool> Delete(int id);
    }
}
