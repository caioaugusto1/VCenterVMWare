using System;
using System.Collections.Generic;
using VSphere.Entities;
using VSphere.Repositories.Interfaces.Base;

namespace VSphere.Repositories.Interfaces
{
    public interface IVMRepository : IRepositoryBaseGET<VMEntity>, IRepositoryBasePOST<VMEntity>
    {
        List<VMEntity> GetByDate(DateTime from, DateTime to);
    }
}
