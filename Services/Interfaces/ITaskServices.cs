using Task_API.Models;

namespace Task_API.Services.Interfaces
{
    public interface ITaskServices
    {
        Task<List<Models.Task>> GetAll();
        Task<Models.Task> GetById(int id);
        Task<Models.Task> Add(Models.Task obj);
        Task<Models.Task> Update(Models.Task obj);
        Task<bool> Delete(int id);
        Task<bool> IsAvailable(int userId, DateTime startDate, DateTime endDate, int? taskId = null);

    }
}
