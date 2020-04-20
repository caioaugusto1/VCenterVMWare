using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using VSphere.Entities;
using VSphere.Repositories.Base;
using VSphere.Repositories.Interfaces;

namespace VSphere.Repositories
{
    public class UserRepository : Repository<UserEntity>, IUserRepository
    {
        public UserRepository(IConfiguration configuration)
            : base(configuration, "user")
        {

        }
        public UserEntity GetByUserAndPassword(string email, string password)
        {
            return _mongoCollection.Find<UserEntity>(user => user.Email == email && user.Password == password).FirstOrDefault();
        }
    }
}
