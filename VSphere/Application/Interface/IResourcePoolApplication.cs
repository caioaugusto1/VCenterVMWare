using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VSphere.Models.ViewModels.ResourcePool;

namespace VSphere.Application.Interface
{
    public interface IResourcePoolApplication
    {
        Task<List<ResourcePoolViewModel>> GetAllByApi(string apiId);
    }
}
