using VCenter.Models;
using VCenter.Repositories.Base;
using VCenter.Repositories.Interfaces;

namespace VCenter.Repositories
{
    public class UserRepository : Repository<UserEntity>, IUserRepository
    {
  
        //private readonly IMongoCollection<Login> _mongoCollection;

        //public LoginRepository(IMongoCollection<Login> mongoCollection, IConfiguration configuration) 
        //    : base(mongoCollection, configuration)
        //{
        //    mongoCollection = _mongoCollection;
        //}

        public void GetUserForLogin(string user, string password)
        {
            
        }
    }
}
