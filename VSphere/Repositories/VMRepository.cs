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
    public class VMRepository : Repository<VMEntity>, IVMRepository
    {
        public VMRepository(IConfiguration configuration)
            : base(configuration, "vm")
        {

        }

        public List<VMEntity> GetByDate(DateTime from, DateTime to)
        {
            return _mongoCollection.Find<VMEntity>(vm => vm.Insert >= from && vm.Insert <= to).ToList();
        }

        public async Task<List<VMEntity>> GetByOrigem(string ip)
        {
            return await _mongoCollection.FindAsync<VMEntity>(vm => vm.Origem == ip).Result.ToListAsync();
        }

        public Task InsertMany(List<VMEntity> entitys)
        {
            return _mongoCollection.InsertManyAsync(entitys);
        }
    }
}
