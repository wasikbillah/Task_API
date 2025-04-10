using Microsoft.EntityFrameworkCore;
using System.Security;
using Task_API.Data;
using Task_API.Models;
using Task_API.Services.Interfaces;

namespace Task_API.Services.Implementations
{
    public class TaskStatusServices : ITaskStatusServices
    {
        private readonly ApplicationDbContext _context;
        public TaskStatusServices(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Models.TaskStatus> Add(Models.TaskStatus obj)
        {
            await _context.TaskStatuses.AddAsync(obj);
            await _context.SaveChangesAsync();       
            return obj;
        }


        public async Task<bool> Delete(int id)
        {
           var exist = await _context.TaskStatuses.FindAsync(id);

           if (exist != null)
           {
                _context.TaskStatuses.Remove(exist);
                await _context.SaveChangesAsync();
                return true;
           }
           
           return false;
        }

        public async Task<List<Models.TaskStatus>> GetAll()
        {
           return  await _context.TaskStatuses.ToListAsync();
        }

        public async Task<Models.TaskStatus> GetById(int id)
        {
            return await _context.TaskStatuses.FirstOrDefaultAsync(x=> x.Id == id);
        }

        public async Task<Models.TaskStatus> Update(Models.TaskStatus obj)
        {
            var exist = await _context.TaskStatuses.FindAsync(obj.Id);
            _context.Entry(exist).CurrentValues.SetValues(obj);
            await _context.SaveChangesAsync();
            return obj;
        }
    }
}
