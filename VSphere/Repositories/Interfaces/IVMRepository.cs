using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VSphere.Entities;
using VSphere.Repositories.Interfaces.Base;

namespace VSphere.Repositories.Interfaces
{
    public interface IVMRepository : IRepositoryBaseGET<VMEntity>, IRepositoryBasePOST<VMEntity>
    {
        Task<List<VMEntity>> GetByDate(string ip, DateTime from, DateTime to);

        Task<List<VMEntity>> GetByOrigem(string ip);

        Task InsertMany(List<VMEntity> entitys);
    }
}
