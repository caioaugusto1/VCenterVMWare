using VSphere.Entities;
using VSphere.Repositories.Interfaces.Base;

namespace VSphere.Repositories.Interfaces
{
    public interface IServerRepository : IRepositoryBaseGET<ServerEntity>, IRepositoryBasePOST<ServerEntity>
    {

    }
}
