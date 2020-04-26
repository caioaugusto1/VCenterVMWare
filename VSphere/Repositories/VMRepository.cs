using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<List<VMEntity>> GetByDate(string ip, DateTime from, DateTime to)
        {
            var allColletioctions = await _mongoCollection.Find<VMEntity>(vm => vm.Origem == ip).ToListAsync();

            allColletioctions = allColletioctions.Where(x => x.Insert >= from && x.Insert <= to).ToList();

            return allColletioctions;
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
