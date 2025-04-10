using Task_API.Models;

namespace Task_API.Services.Interfaces
{
    public interface ITaskStatusServices
    {
        Task<List<Models.TaskStatus>> GetAll();
        Task<Models.TaskStatus> GetById(int id);
        Task<Models.TaskStatus> Add(Models.TaskStatus obj);
        Task<Models.TaskStatus> Update(Models.TaskStatus obj);
        Task<bool> Delete(int id);
    }
}
