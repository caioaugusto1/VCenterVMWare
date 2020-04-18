using MongoDB.Bson.Serialization.Attributes;
using System;
using VSphere.Entities.Base;

namespace VSphere.Entities
{
    public class HostEntity : EntityBase
    {
        [BsonElement("host")]
        public string Host { get; private set; }

        [BsonElement("name")]
        public string Name { get; private set; }

        [BsonElement("connection_state")]
        public string State { get; private set; }

        [BsonElement("power_state")]
        public string Power { get; private set; }

        [BsonElement("origem")]
        public string Origem { get; private set; }

        [BsonElement("insert")]
        public DateTime Insert { get; private set; }


        public HostEntity(string host, string name, string state, string power, string origem)
        {
            Host = host;
            Name = name;
            State = state;
            Power = power;
            Origem = origem;
            Insert = DateTime.Now;
        }
    }
}
