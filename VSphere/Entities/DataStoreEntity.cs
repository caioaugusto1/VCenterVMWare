using MongoDB.Bson.Serialization.Attributes;
using System;
using VSphere.Entities.Base;

namespace VSphere.Entities
{
    public class DataStoreEntity : EntityBase
    {

        [BsonElement("datastore")]
        public string Datastore { get; private set; }

        [BsonElement("name")]
        public string Name { get; private set; }

        [BsonElement("type")]
        public string Type { get; private set; }

        [BsonElement("free_space")]
        public string FreeSpace { get; private set; }

        [BsonElement("capacity")]
        public string Capacity { get; private set; }

        [BsonElement("origem")]
        public string Origem { get; private set; }

        [BsonElement("Insert")]
        public DateTime Insert { get; private set; }

        public DataStoreEntity(string datastore, string name, string type, string freeSpace, string origem, string capacity)
        {
            Datastore = datastore;
            Name = name;
            Type = type;
            FreeSpace = freeSpace;
            Capacity = capacity;
            Origem = origem;
            Insert = DateTime.Now;
        }

    }
}
