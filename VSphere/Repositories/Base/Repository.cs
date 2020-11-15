using AutoMapper.Configuration;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using VSphere.Repositories.Interfaces.Base;

namespace VSphere.Repositories.Base
{
    public abstract class Repository<TEntity> : IDisposable,
        IRepositoryConnection<TEntity>,
        IRepositoryBasePOST<TEntity>,
        IRepositoryBaseGET<TEntity> where TEntity : class
    {
        protected readonly IMongoCollection<TEntity> _mongoCollection;

        protected readonly Microsoft.Extensions.Configuration.IConfiguration _configuration;

        public IMongoCollection<TEntity> Connection(Microsoft.Extensions.Configuration.IConfiguration configuration, string collectionName)
        {
            MongoClient client = new MongoClient(configuration.GetConnectionString("VsphereMongoConnection"));
            //IMongoDatabase database = client.GetDatabase("VsphereMongoDatabaseName");

            IMongoDatabase database = client.GetDatabase(configuration.GetConnectionString("VsphereMongoDatabaseName"));
            return database.GetCollection<TEntity>(collectionName);
        }

        protected Repository(Microsoft.Extensions.Configuration.IConfiguration configuration, string collectionName)
        {
            _configuration = configuration;
            _mongoCollection = Connection(_configuration, collectionName);
        }

        public async Task<bool> Delete(string id)
        {
            var deleteResult = await _mongoCollection.DeleteOneAsync(Builders<TEntity>.Filter.AnyEq("_id", ObjectId.Parse(id)));

            return deleteResult.DeletedCount > 1;
        }

        public void Dispose()
        {

        }

        public async Task<List<TEntity>> FindByFilter(Expression<Func<TEntity, bool>> predicate)
        {
            return _mongoCollection.AsQueryable<TEntity>().AsQueryable()
                       .Where(predicate).ToList();
        }

        public async Task<List<TEntity>> GetAll()
        {
            return _mongoCollection.Find<TEntity>(x => true).ToList();
        }

        public async Task<TEntity> GetById(string id)
        {
            return _mongoCollection.Find<TEntity>(Builders<TEntity>.Filter.AnyEq("_id", ObjectId.Parse(id))).FirstOrDefault();
        }

        public async void Insert(TEntity obj)
        {
            await _mongoCollection.InsertOneAsync(obj);
        }

        public async Task<bool> Update(TEntity obj, string id)
        {
            var result = await _mongoCollection.ReplaceOneAsync(Builders<TEntity>.Filter.AnyEq("_id", ObjectId.Parse(id)), obj, new UpdateOptions() { IsUpsert = true });

            //var result = await _mongoCollection.ReplaceOneAsync(Builders<TEntity>.Filter.AnyEq("_id", ObjectId.Parse(id)), obj, new UpdateOptions { IsUpsert = true });

            return result.ModifiedCount > 1;
        }
    }
}
