using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VSphere.Models.Identity;

namespace VSphere.Context
{
    public class VSphereContext : IdentityDbContext<ApplicationIdentityUser, IdentityRole, string>
    {
        public VSphereContext()
        {
                
        }

        public VSphereContext(DbContextOptions<VSphereContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
