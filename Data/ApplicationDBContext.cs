using core_admin.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace core_admin.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Define any additional DbSets here if needed
        public DbSet<Entity> Entities { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}
