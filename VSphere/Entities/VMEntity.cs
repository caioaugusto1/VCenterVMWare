﻿using MongoDB.Bson.Serialization.Attributes;
using System;
using VSphere.Entities.Base;

namespace VSphere.Entities
{
    public class VMEntity : EntityBase
    {
        [BsonElement("memory_size_MiB")]
        public string Memory { get; private set; }

        [BsonElement("vm")]
        public string VM { get; private set; }

        [BsonElement("name")]
        public string Name { get; private set; }

        [BsonElement("power_state")]
        public string Power { get; private set; }

        [BsonElement("cpu_count")]
        public int CPU { get; private set; }

        [BsonElement("origem")]
        public string Origem { get; private set; }

        [BsonElement("insert")]
        public DateTime Insert { get; private set; }

        public VMEntity(string memory, string vm, string name, string power, int cpu, string origem)
        {
            Memory = memory;
            VM = vm;
            Name = name;
            Power = power;
            CPU = cpu;
            Origem = origem;
            Insert = DateTime.Now;
        }
    }
}
