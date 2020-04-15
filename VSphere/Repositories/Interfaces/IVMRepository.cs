using System;
using System.Collections.Generic;
using VCenter.Repositories.Interfaces.Base;
using VSphere.Entities;

namespace VSphere.Repositories.Interfaces
{
    public interface IVMRepository : IRepositoryBaseGET<VMEntity>, IRepositoryBasePOST<VMEntity>
    {
        List<VMEntity> GetByDate(DateTime from, DateTime to);
    }
}
