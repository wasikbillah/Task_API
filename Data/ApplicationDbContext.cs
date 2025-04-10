using Microsoft.EntityFrameworkCore;
using Task_API.Models;

namespace Task_API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Models.TaskStatus> TaskStatuses { get; set; }
        public DbSet<Models.Task> Tasks { get; set; }

    }
}
