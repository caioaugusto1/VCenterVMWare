using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace VSphere.Repositories.Interfaces.Base
{
    public interface IRepositoryBaseGET<TEntity> where TEntity : class
    {
        List<TEntity> GetAll();

        TEntity GetById(string id);

        List<TEntity> FindByFilter(Expression<Func<TEntity, bool>> predicate);
    }
}
