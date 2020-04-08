using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VCenter.Repositories.Interfaces.Base
{
    public interface IRepositoryConnection<TEntity>
    {
        IMongoCollection<TEntity> Connection();
    }
}
