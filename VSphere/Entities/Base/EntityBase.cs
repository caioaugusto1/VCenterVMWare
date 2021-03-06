﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace VSphere.Entities.Base
{
    public abstract class EntityBase
    {

        public EntityBase()
        {
            Id = ObjectId.GenerateNewId().ToString();
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; protected set; } 
    }
}
