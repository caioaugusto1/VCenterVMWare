using System.Collections.Generic;
using System.Threading.Tasks;

namespace VCenter.Services.Inteface
{
    public interface IService<Object> 
    {
        Task<List<Object>> GetAllAsync();
        Task<Object> GetByIdAsync(int id);
        Task<Object> CreateAsync(Object task);
        Task<Object> UpdateAsync(Object task);
        Task RemoveAsync(int id);
    }
}
