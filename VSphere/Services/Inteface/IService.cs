using System.Collections.Generic;
using System.Threading.Tasks;

namespace VSphere.Services.Inteface
{
    public interface IService
    {
        object GetAllAsync();

        void CreateClient();
        string GetSession();

        string UserStringBase64();
    }
}
