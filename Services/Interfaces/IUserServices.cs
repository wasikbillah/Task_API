using Task_API.Models;

namespace Task_API.Services.Interfaces
{
    public interface IUserServices
    {
        Task<List<User>> GetAll();
        Task<User> GetById(int id);
        Task<User> Add(User obj);
        Task<User> Update(User obj);
        Task<bool> Delete(int id);
    }
}
