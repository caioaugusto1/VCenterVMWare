using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace VSphere.Repositories.Interfaces.Base
{
    public interface IRepositoryBaseGET<TEntity> where TEntity : class
    {
        Task<List<TEntity>> GetAll();

        Task<TEntity> GetById(string id);

        Task<List<TEntity>> FindByFilter(Expression<Func<TEntity, bool>> predicate);
    }
}
