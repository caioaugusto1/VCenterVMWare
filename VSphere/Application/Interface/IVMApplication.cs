using System.Collections.Generic;
using System.Threading.Tasks;
using VSphere.Models;

namespace VSphere.Application.Interface
{
    public interface IVMApplication
    {
        Task<List<VMViewModel>> GetAllByDate(string apiId, string from, string to);

        Task<List<VMViewModel>> GetAllByApi(string apiId);

        byte[] PDFGenerator(string html);
    }
}
