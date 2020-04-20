using AutoMapper.Configuration;
using MongoDB.Driver;

namespace VSphere.Repositories.Interfaces.Base
{
    public interface IRepositoryConnection<TEntity>
    {
        IMongoCollection<TEntity> Connection(Microsoft.Extensions.Configuration.IConfiguration configuration, string collectioName);
    }
}
