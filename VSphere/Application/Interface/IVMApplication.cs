using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using VSphere.Models;
using VSphere.Models.JsonConvert;

namespace VSphere.Application.Interface
{
    public interface IVMApplication
    {
        Task<List<VMViewModel>> GetAllByDate(string apiId, string from, string to);

        Task<List<VMViewModel>> GetAllByApi(string apiId);

        byte[] PDFGenerator(string html);
    }
}
