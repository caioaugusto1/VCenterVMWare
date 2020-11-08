using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using VSphere.Models;

namespace VSphere.Application.Interface
{
    public interface IVMApplication
    {
        Task<List<VMViewModel>> GetAllByDate(string apiId, string from, string to);

        Task<List<VMViewModel>> GetAllByApi(string apiId);

        Task<HttpStatusCode> Delete(string apiId, string name);

        byte[] PDFGenerator(string html);
    }
}
