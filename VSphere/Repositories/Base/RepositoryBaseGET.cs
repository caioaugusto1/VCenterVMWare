﻿using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using VSphere.Repositories.Interfaces.Base;

namespace VSphere.Repositories.Base
{
    public class RepositoryBaseGET<TEntity> : Repository<TEntity>, IRepositoryBaseGET<TEntity> where TEntity : class
    {
        public RepositoryBaseGET(string collectionName) : base(collectionName)
        {
        }

        public List<TEntity> FindByFilter(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public List<TEntity> GetAll()
        {
            return _mongoCollection.Find<TEntity>(x => true).ToList();
        }

        public TEntity GetById(string id)
        {
            return _mongoCollection.Find<TEntity>(Builders<TEntity>.Filter.AnyEq("_id", ObjectId.Parse(id))).FirstOrDefault();
        }
    }
}
