using Microsoft.Extensions.Configuration;
using VSphere.Entities;
using VSphere.Repositories.Base;
using VSphere.Repositories.Interfaces;

namespace VSphere.Repositories
{
    public class ServerRepository : Repository<ServerEntity>, IServerRepository
    {
        public ServerRepository(IConfiguration configuration) : base(configuration, "server")
        {
        }
    }
}
