using System.Collections.Generic;
using System.Threading.Tasks;
using VSphere.Models;

namespace VSphere.Application.Interface
{
    public interface IServerApplication
    {
        Task<List<ServerViewModel>> GetAll();

        Task<ServerViewModel> GetById(string id);

        void Insert(ServerViewModel obj);

        Task<bool> Update(string id, ServerViewModel obj);

        Task<bool> Delete(string id);
    }
}
