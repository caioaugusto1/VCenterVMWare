using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VSphere.Models;

namespace VSphere.Application.Interface
{
    public interface IHostApplication
    {
        List<HostViewModel> GetAll();
    }
}
