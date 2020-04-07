using VCenter.Models;
using VCenter.Repositories.Interfaces.Base;

namespace VCenter.Repositories.Interfaces
{
    public interface IUserRepository : IRepositoryBaseGET<UserEntity>, IRepositoryBasePOST<UserEntity>
    {
        void GetUserForLogin(string user, string password);
    }
}
