using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using VCenter.Entities;
using VCenter.Models;
using VCenter.Repositories.Base;
using VCenter.Repositories.Interfaces;

namespace VCenter.Repositories
{
    public class UserRepository : Repository<UserEntity>, IUserRepository
    {

        private readonly IMongoCollection<UserEntity> _mongoCollection;

        public UserRepository()
        {
            _mongoCollection = Connection();
        }

        public UserEntity GetByUserAndPassword(string email, string password)
        {
            return _mongoCollection.Find<UserEntity>(user => user.Email == email && user.Password == password).FirstOrDefault();
        }
    }
}
