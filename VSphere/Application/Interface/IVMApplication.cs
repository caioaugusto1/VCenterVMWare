using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VSphere.Models;
using VSphere.Models.JsonConvert;

namespace VSphere.Application.Interface
{
    public interface IVMApplication
    {
        List<VMViewModel> GetAll(string apiId);

        List<VMViewModel> GetAllByDate(DateTime from, DateTime to);

        Task<List<VMViewModel>> GetAllByApi(string apiId);
    }
}
