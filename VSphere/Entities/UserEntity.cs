using MongoDB.Bson.Serialization.Attributes;
using System;
using VSphere.Entities.Base;

namespace VSphere.Entities
{
    public class UserEntity : EntityBase
    {

        [BsonElement("fullName")]
        public string FullName { get; private set; }

        [BsonElement("email")]
        public string Email { get; private set; }

        [BsonElement("password")]
        public string Password { get; private set; }

        [BsonElement("insert")]
        public DateTime Insert { get; private set; }

        [BsonElement("block")]
        public DateTime Block { get; private set; }

        [BsonElement("active")]
        public bool Active { get; private set; }

        public UserEntity(string fullName, string email, string password)
        {
            FullName = fullName;
            Email = email;
            Password = password;
            Insert = DateTime.Now;
        }

        public void SetInactive(UserEntity user)
        {
            user.Active = false;
        }

        public void SetBlocker(UserEntity user)
        {
            user.Block = DateTime.Now;
        }
    }
}
