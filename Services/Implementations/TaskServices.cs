using Microsoft.EntityFrameworkCore;
using System.Security;
using Task_API.Data;
using Task_API.Models;
using Task_API.Services.Interfaces;

namespace Task_API.Services.Implementations
{
    public class TaskServices : ITaskServices
    {
        private readonly ApplicationDbContext _context;
        public TaskServices(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Models.Task> Add(Models.Task obj)
        {
            await _context.Tasks.AddAsync(obj);
            await _context.SaveChangesAsync();       
            return obj;
        }


        public async Task<bool> Delete(int id)
        {
           var exist = await _context.Tasks.FindAsync(id);

           if (exist != null)
           {
                _context.Tasks.Remove(exist);
                await _context.SaveChangesAsync();
                return true;
           }
           
           return false;
        }

        public async Task<List<Models.Task>> GetAll()
        {
           return  await _context.Tasks.Include(u => u.AssignedUser).Include(x=> x.Status).ToListAsync();
        }

        public async Task<Models.Task> GetById(int id)
        {
            return await _context.Tasks.FirstOrDefaultAsync(x=> x.Id == id);
        }

        public async Task<Models.Task> Update(Models.Task obj)
        {
            var exist = await _context.Tasks.FindAsync(obj.Id);
            _context.Entry(exist).CurrentValues.SetValues(obj);
            await _context.SaveChangesAsync();
            return obj;
        }
    }
}
