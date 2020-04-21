using System.Collections.Generic;
using System.Threading.Tasks;
using VSphere.Models;
using VSphere.Models.JsonConvert;

namespace VSphere.Application.Interface
{
    public interface IDataStoreApplication
    {
        List<DataStoreViewModel> GetAll(string apiId);

        Task<List<DataStoreViewModel>> GetAllByApi(string apiId);
    }
}
