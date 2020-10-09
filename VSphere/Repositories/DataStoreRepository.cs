using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VSphere.Entities;
using VSphere.Repositories.Base;
using VSphere.Repositories.Interfaces;

namespace VSphere.Repositories
{
    public class DataStoreRepository : Repository<DataStoreEntity>, IDataStoreRepository
    {
        public DataStoreRepository(IConfiguration configuration)
            : base(configuration, "datastore")
        {
        }

        public async Task<List<DataStoreEntity>> GetByOrigem(string ip, DateTime from, DateTime to)
        {
            var getAll = await _mongoCollection.Find<DataStoreEntity>(dataStore => dataStore.Origem == ip && dataStore.Insert >= to && dataStore.Insert <= from).ToListAsync();

            return getAll;
        }

        public async Task InsertMany(List<DataStoreEntity> entitys)
        {
            await _mongoCollection.InsertManyAsync(entitys);
        }
    }
}
