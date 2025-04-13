using Microsoft.EntityFrameworkCore;
using System.Security;
using Task_API.Data;
using Task_API.Models;
using Task_API.Services.Interfaces;

namespace Task_API.Services.Implementations
{
    public class UserServices : IUserServices
    {
        private readonly ApplicationDbContext _context;
        public UserServices(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<User> Add(User obj)
        {
            await _context.Users.AddAsync(obj);
            await _context.SaveChangesAsync();       
            return obj;
        }


        public async Task<bool> Delete(int id)
        {
           var exist = await _context.Users.FindAsync(id);

           if (exist != null)
           {
                _context.Users.Remove(exist);
                await _context.SaveChangesAsync();
                return true;
           }
           
           return false;
        }

        public async Task<List<User>> GetAll()
        {
         return await _context.Users.Include(u => u.Role).ToListAsync();
        }

        public async Task<User> GetById(int id)
        {
            return await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(x => x.Id == id);
        }


        public async Task<User> Update(User obj)
        {
            var exist = await _context.Users.FindAsync(obj.Id);
            if (exist == null)
            {
                return null;
            }
          

            exist.Name = obj.Name;
            exist.RoleId = obj.RoleId;

            if (obj.Password == "")
            {
                exist.Password = exist.Password;
            }
            else
            {
                exist.Password = obj.Password;
            }

            await _context.SaveChangesAsync();
            return exist;
        }

    }
}
