using Microsoft.Extensions.Configuration;
using VSphere.Entities;
using VSphere.Repositories.Base;
using VSphere.Repositories.Interfaces;

namespace VSphere.Repositories
{
    public class HostRepository : Repository<HostEntity>, IHostRepository
    {
        public HostRepository(IConfiguration configuration)
            : base(configuration, "host")
        {

        }
    }
}
