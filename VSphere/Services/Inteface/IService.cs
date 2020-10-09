using System.Threading.Tasks;
using VSphere.Models.JsonConvert;

namespace VSphere.Services.Inteface
{
    public interface IService
    {
        Task<VMConvert> GetVMsAPI(string url, string username, string password);

        Task<HostConvert> GetHostsAPI(string url, string username, string password);

        Task<DataStoreConvert> GetDataStoreAPI(string url, string username, string password);

        void SendEmail(string to, string filename = "");

        byte[] GetFile(string fileName);

        string PDFGenerator(string html);

    }
}
