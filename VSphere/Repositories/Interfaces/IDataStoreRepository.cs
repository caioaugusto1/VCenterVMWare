using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VSphere.Entities;
using VSphere.Repositories.Interfaces.Base;

namespace VSphere.Repositories.Interfaces
{
    public interface IDataStoreRepository : IRepositoryBaseGET<DataStoreEntity>, IRepositoryBasePOST<DataStoreEntity>
    {
        Task InsertMany(List<DataStoreEntity> entitys);

        Task<List<DataStoreEntity>> GetByOrigem(string ip, DateTime from, DateTime to);
    }
}
