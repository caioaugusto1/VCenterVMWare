using System.Collections.Generic;
using VSphere.Models;

namespace VSphere.Application.Interface
{
    public interface IServerApplication
    {
        List<ServerViewModel> GetAll();
    }
}
