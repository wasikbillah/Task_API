using Microsoft.EntityFrameworkCore;
using System.Security;
using Task_API.Data;
using Task_API.Models;
using Task_API.Services.Interfaces;

namespace Task_API.Services.Implementations
{
    public class RolePermissionServices : IRolePermissionServices
    {
        private readonly ApplicationDbContext _context;
        public RolePermissionServices(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<RolePermission> Add(RolePermission obj)
        {
            await _context.RolePermissions.AddAsync(obj);
            await _context.SaveChangesAsync();       
            return obj;
        }


        public async Task<bool> Delete(int id)
        {
           var exist = await _context.RolePermissions.FindAsync(id);

           if (exist != null)
           {
                _context.RolePermissions.Remove(exist);
                await _context.SaveChangesAsync();
                return true;
           }
           
           return false;
        }

        public async Task<List<RolePermission>> GetAll()
        {
            return await _context.RolePermissions.Include(u => u.Role).Include(x=> x.Permission).ToListAsync();
        }

        public async Task<List<RolePermission>> GetById(int roleId)
        {
            return await _context.RolePermissions
                .Where(x => x.RoleId == roleId)
                .Include(x => x.Role)      
                .Include(x => x.Permission) 
                .ToListAsync();
        }

        public async Task<RolePermission> Update(RolePermission obj)
        {
            var exist = await _context.RolePermissions.FindAsync(obj.Id);
            if (exist == null)
            {
                return null;
            }

            exist.PermissionId = obj.PermissionId;
            exist.RoleId = obj.RoleId;

            await _context.SaveChangesAsync();
            return exist;
        }
    }
}
