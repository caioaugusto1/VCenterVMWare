using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using VSphere.Repositories.Interfaces.Base;

namespace VSphere.Repositories.Base
{
    public abstract class Repository<TEntity> : IDisposable,
        IRepositoryConnection<TEntity>,
        IRepositoryBasePOST<TEntity>,
        IRepositoryBaseGET<TEntity> where TEntity : class
    {
        protected readonly IMongoCollection<TEntity> _mongoCollection;

        protected readonly IConfiguration configuration;

        public IMongoCollection<TEntity> Connection(string collectionName)
        {
            //MongoClient client = new MongoClient(configuration.GetConnectionString("VCenterMongoDBDatabase"));
            MongoClient client = new MongoClient("mongodb://localhost:27017");
            IMongoDatabase database = client.GetDatabase("VCenter");

            return database.GetCollection<TEntity>(collectionName);
        }

        protected Repository(string collectionName)
        {
            _mongoCollection = Connection(collectionName);
        }

        public void Delete(string id)
        {
            _mongoCollection.DeleteOne(Builders<TEntity>.Filter.AnyEq("_id", ObjectId.Parse(id)));
        }

        public void Dispose()
        {

        }

        public List<TEntity> FindByFilter(Expression<Func<TEntity, bool>> predicate)
        {
            return _mongoCollection.AsQueryable<TEntity>().AsQueryable()
                       .Where(predicate).ToList();
        }

        public List<TEntity> GetAll()
        {
            return _mongoCollection.Find<TEntity>(x => true).ToList();
        }

        public TEntity GetById(string id)
        {
            return _mongoCollection.Find<TEntity>(Builders<TEntity>.Filter.AnyEq("_id", ObjectId.Parse(id))).FirstOrDefault();
        }

        public void Insert(TEntity obj)
        {
            _mongoCollection.InsertOne(obj);
        }

        public void Update(TEntity obj, string id)
        {
            _mongoCollection.ReplaceOne(Builders<TEntity>.Filter.AnyEq("_id", ObjectId.Parse(id)), obj);
        }
    }
}
