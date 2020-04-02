using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;
using VCenter.Repositories.Interfaces.Base;

namespace VCenter.Repositories.Base
{
    public class RepositoryBasePOST<TEntity> : Repository<TEntity>, IRepositoryBasePOST<TEntity> where TEntity : class
    {
        public void Insert(TEntity obj)
        {
            _mongoCollection.InsertOne(obj);
        }

        public void Update(TEntity obj, string id)
        {
            _mongoCollection.ReplaceOne(Builders<TEntity>.Filter.AnyEq("_id", ObjectId.Parse(id)), obj);
        }

        public void Delete(string id)
        {
            _mongoCollection.DeleteOne(Builders<TEntity>.Filter.AnyEq("_id", ObjectId.Parse(id)));
        }
    }
}
