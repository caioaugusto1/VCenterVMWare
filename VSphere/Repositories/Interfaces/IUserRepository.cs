using VSphere.Entities;
using VSphere.Repositories.Interfaces.Base;

namespace VSphere.Repositories.Interfaces
{
    public interface IUserRepository : IRepositoryBaseGET<UserEntity>, IRepositoryBasePOST<UserEntity>
    {
        UserEntity GetByUserAndPassword(string email, string password);
    }
}
