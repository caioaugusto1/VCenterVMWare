using System.Net;
using System.Threading.Tasks;
using VSphere.Models.JsonConvert;

namespace VSphere.Services.Inteface
{
    public interface IService
    {
        Task<VMConverter> GetVMsAPI(string url, string username, string password);

        Task<HostConverter> GetHostsAPI(string url, string username, string password);

        Task<HttpStatusCode> CreateVM(string url, string username, string password, VMPost vmModelConvertered);


        Task<DataStoreConvert> GetDataStoreAPI(string url, string username, string password);

        Task<FolderConverter> GetFolderAPI(string url, string username, string password);

        Task<ResourcePoolConverter> GetResourcePoolAPI(string url, string username, string password);

        Task<HttpStatusCode> DeleteVMAPI(string url, string username, string password, string name);

        Task<HttpStatusCode> TurnOnOrTurnOffVMAPI(string url, string username, string password, string name, bool turnOn);

        byte[] GetFile(string fileName);

        string PDFGenerator(string html);

    }
}
