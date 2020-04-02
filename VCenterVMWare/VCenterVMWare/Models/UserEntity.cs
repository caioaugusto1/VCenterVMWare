using MongoDB.Bson.Serialization.Attributes;
using System;

namespace VCenter.Models
{
    public class UserEntity : EntityBase
    {
        [BsonElement("firstname")]
        public string FirstName { get; private set; }

        [BsonElement("surname")]
        public string Surname { get; private set; }

        [BsonElement("email")]
        public string Email { get; private set; }

        [BsonElement("password")]
        public string Password { get; private set; }

        [BsonElement("insert")]
        public DateTime Insert { get; private set; }

        public UserEntity(string firstName, string surName, string email, string password)
        {
            FirstName = firstName;
            Surname = surName;
            Email = email;
            Password = password;
            Insert = DateTime.Now;
        }
    }
}
