using VSphere.Entities;
using VSphere.Repositories.Interfaces.Base;

namespace VSphere.Repositories.Interfaces
{
    public interface IHostRepository : IRepositoryBaseGET<HostEntity>, IRepositoryBasePOST<HostEntity>
    {
    }
}
