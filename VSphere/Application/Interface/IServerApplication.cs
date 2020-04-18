using System.Collections.Generic;
using System.Threading.Tasks;
using VSphere.Models;

namespace VSphere.Application.Interface
{
    public interface IServerApplication
    {
        List<ServerViewModel> GetAll();

        ServerViewModel GetById(string id);

        void Insert(ServerViewModel obj);

    }
}
