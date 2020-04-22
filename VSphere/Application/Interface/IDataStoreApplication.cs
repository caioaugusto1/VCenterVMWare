using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VSphere.Models;

namespace VSphere.Application.Interface
{
    public interface IDataStoreApplication
    {
        List<DataStoreViewModel> GetAll(string apiId, DateTime from, DateTime to);

        Task<List<DataStoreViewModel>> GetAllByApi(string apiId);
    }
}
