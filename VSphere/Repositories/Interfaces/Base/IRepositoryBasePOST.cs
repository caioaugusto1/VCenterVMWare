using System.Threading.Tasks;

namespace VSphere.Repositories.Interfaces.Base
{
    public interface IRepositoryBasePOST<TEntity> where TEntity : class
    {
        void Insert(TEntity obj);

        Task<bool> Update(TEntity obj, string id);

        Task<bool> Delete(string id);
    }
}
