using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace VSphere.Context
{
    public class VSphereContext : IdentityDbContext
    {
        public VSphereContext(DbContextOptions<VSphereContext> options)
            : base(options)
        {
        }
    }
}
