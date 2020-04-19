using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VSphere.Entities;
using VSphere.Repositories.Interfaces.Base;

namespace VSphere.Repositories.Interfaces
{
    public interface IVMRepository : IRepositoryBaseGET<VMEntity>, IRepositoryBasePOST<VMEntity>
    {
        List<VMEntity> GetByDate(DateTime from, DateTime to);

        Task<List<VMEntity>> GetByOrigem(string ip);

        Task InsertMany(List<VMEntity> entitys);
    }
}
