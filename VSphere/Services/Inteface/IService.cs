using System.Collections.Generic;
using System.Threading.Tasks;

namespace VSphere.Services.Inteface
{
    public interface IService<Object>
    {
        void Run();
        Task<List<Object>> GetAllAsync();
        Task<Object> GetByIdAsync(int id);
        Task<Object> CreateAsync(Object task);
        Task<Object> UpdateAsync(Object task);
        Task RemoveAsync(int id);
    }
}
