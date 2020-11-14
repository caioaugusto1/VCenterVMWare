using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using VSphere.Models;
using VSphere.Models.ViewModels.VM;

namespace VSphere.Application.Interface
{
    public interface IVMApplication
    {
        Task<List<VMViewModel>> GetAllByDate(string apiId, string from, string to);

        Task<HttpStatusCode> Create(string apiId, CreateVMViewModel model);

        Task<List<VMViewModel>> GetAllByApi(string apiId);

        Task<HttpStatusCode> Delete(string apiId, string name);

        Task<HttpStatusCode> TurnOnOrTurnOff(string apiId, string name, bool turnOn);

        byte[] PDFGenerator(string html);
    }
}
