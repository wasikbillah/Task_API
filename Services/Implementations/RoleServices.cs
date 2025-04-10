using Microsoft.EntityFrameworkCore;
using System.Security;
using Task_API.Data;
using Task_API.Models;
using Task_API.Services.Interfaces;

namespace Task_API.Services.Implementations
{
    public class RoleServices : IRoleServices
    {
        private readonly ApplicationDbContext _context;
        public RoleServices(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Role> Add(Role obj)
        {
            await _context.Roles.AddAsync(obj);
            await _context.SaveChangesAsync();       
            return obj;
        }


        public async Task<bool> Delete(int id)
        {
           var exist = await _context.Roles.FindAsync(id);

           if (exist != null)
           {
                _context.Roles.Remove(exist);
                await _context.SaveChangesAsync();
                return true;
           }
           
           return false;
        }

        public async Task<List<Role>> GetAll()
        {
           return  await _context.Roles.ToListAsync();
        }

        public async Task<Role> GetById(int id)
        {
            return await _context.Roles.FirstOrDefaultAsync(x=> x.Id == id);
        }

        public async Task<Role> Update(Role obj)
        {
            var exist = await _context.Roles.FindAsync(obj.Id);
            _context.Entry(exist).CurrentValues.SetValues(obj);
            await _context.SaveChangesAsync();
            return obj;
        }
    }
}
