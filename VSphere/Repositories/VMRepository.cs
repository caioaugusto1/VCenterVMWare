using MongoDB.Driver;
using System;
using System.Collections.Generic;
using VSphere.Entities;
using VSphere.Repositories.Base;
using VSphere.Repositories.Interfaces;

namespace VSphere.Repositories
{
    public class VMRepository : Repository<VMEntity>, IVMRepository
    {
        public VMRepository()
            : base ("vm")
        {

        }

        public List<VMEntity> GetByDate(DateTime from, DateTime to)
        {
            return _mongoCollection.Find<VMEntity>(vm => vm.Insert >= from && vm.Insert <= to).ToList();
        }
    }
}
