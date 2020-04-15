using VCenter.Entities;
using VCenter.Repositories.Interfaces.Base;

namespace VCenter.Repositories.Interfaces
{
    public interface IUserRepository : IRepositoryBaseGET<UserEntity>, IRepositoryBasePOST<UserEntity>
    {
        UserEntity GetByUserAndPassword(string email, string password);
    }
}
