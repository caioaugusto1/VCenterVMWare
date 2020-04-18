using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VSphere.Repositories.Interfaces.Base
{
    public interface IRepositoryConnection<TEntity>
    {
        IMongoCollection<TEntity> Connection(string collectioName);
    }
}
