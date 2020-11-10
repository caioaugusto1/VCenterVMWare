using System.Collections.Generic;
using System.Threading.Tasks;
using VSphere.Models.ViewModels.Folder;

namespace VSphere.Application.Interface
{
    public interface IFolderApplication
    {
        Task<List<FolderViewModel>> GetAllByApi(string apiId);

    }
}
