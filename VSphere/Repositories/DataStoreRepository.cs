using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using VSphere.Entities;
using VSphere.Repositories.Base;
using VSphere.Repositories.Interfaces;

namespace VSphere.Repositories
{
    public class DataStoreRepository : Repository<DataStoreEntity>, IDataStoreRepository
    {
        public DataStoreRepository(IConfiguration configuration, string collectionName)
            : base(configuration, "datastore")
        {
        }

        public Task InsertMany(List<DataStoreEntity> entitys)
        {
            return _mongoCollection.InsertManyAsync(entitys);
        }
    }
}
