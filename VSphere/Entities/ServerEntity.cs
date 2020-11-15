using MongoDB.Bson.Serialization.Attributes;
using VSphere.Entities.Base;

namespace VSphere.Entities
{
    public class ServerEntity : EntityBase
    {
        [BsonElement("ip")]
        public string IP { get; private set; }

        [BsonElement("username")]
        public string UserName { get; private set; }

        [BsonElement("password")]
        public string Password { get; private set; }

        [BsonElement("description")]
        public string Description { get; private set; }

        public ServerEntity(string iP, string userName, string password, string description)
        {
            IP = iP;
            UserName = userName;
            Password = password;
            Description = description;
        }

        public ServerEntity Update(string iP, string userName, string password, string description)
        {
            this.IP = iP;
            this.IP = userName;
            this.IP = password;
            this.IP = description;

            return this;
        }
    }
}
