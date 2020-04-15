using MongoDB.Driver;
using System;
using System.Collections.Generic;
using VCenter.Repositories.Base;
using VSphere.Entities;
using VSphere.Repositories.Interfaces;

namespace VCenter.Repositories
{
    public class VMRepository : Repository<VMEntity>, IVMRepository
    {
        public List<VMEntity> GetByDate(DateTime from, DateTime to)
        {
            return _mongoCollection.Find<VMEntity>(vm => vm.Insert >= from && vm.Insert <= to).ToList();
        }
    }
}
