using MongoDB.Driver;
using VCenter.Entities;
using VCenter.Repositories.Base;
using VCenter.Repositories.Interfaces;

namespace VCenter.Repositories
{
    public class UserRepository : Repository<UserEntity>, IUserRepository
    {
        public UserEntity GetByUserAndPassword(string email, string password)
        {
            return _mongoCollection.Find<UserEntity>(user => user.Email == email && user.Password == password).FirstOrDefault();
        }
    }
}
