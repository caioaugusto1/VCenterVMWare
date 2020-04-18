using System.Threading.Tasks;

namespace VSphere.Repositories.Interfaces.Base
{
    public interface IRepositoryBasePOST<TEntity> where TEntity : class
    {
        void Insert(TEntity obj);

        void Update(TEntity obj, string id);

        void Delete(string id);
    }
}
