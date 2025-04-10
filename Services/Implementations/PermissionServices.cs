using Microsoft.EntityFrameworkCore;
using System.Security;
using Task_API.Data;
using Task_API.Models;
using Task_API.Services.Interfaces;

namespace Task_API.Services.Implementations
{
    public class PermissionServices : IPermissionServices
    {
        private readonly ApplicationDbContext _context;
        public PermissionServices(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Permission> Add(Permission obj)
        {
            await _context.Permissions.AddAsync(obj);
            await _context.SaveChangesAsync();       
            return obj;
        }


        public async Task<bool> Delete(int id)
        {
            var exist = await _context.Permissions.FindAsync(id);

            if (exist != null)
            {
                _context.Permissions.Remove(exist);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }


        public async Task<List<Permission>> GetAll()
        {
           return  await _context.Permissions.ToListAsync();
        }

        public async Task<Permission> GetById(int id)
        {
            return await _context.Permissions.FirstOrDefaultAsync(x=> x.Id == id);
        }

        public async Task<Permission> Update(Permission obj)
        {
            var exist = await _context.Permissions.FindAsync(obj.Id);
            _context.Entry(exist).CurrentValues.SetValues(obj);
            await _context.SaveChangesAsync();
            return obj;
        }
    }
}
