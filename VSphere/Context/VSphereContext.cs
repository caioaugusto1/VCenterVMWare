using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VSphere.Models.Identity;

namespace VSphere.Context
{
    public class VSphereContext : IdentityDbContext<ApplicationIdentityUser>
    {
        public VSphereContext(DbContextOptions<VSphereContext> options)
            : base(options)
        {
        }
    }
}
