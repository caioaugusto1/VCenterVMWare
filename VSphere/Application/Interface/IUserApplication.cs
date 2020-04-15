using System.Collections.Generic;
using VCenter.Models;
using VSphere.Models;

namespace VCenterVMWare.Application.Inteface
{
    public interface IUserApplication
    {
        UserViewModel GetByUserAndPassword(string user, string password);

        List<UserViewModel> GetAll();

        UserViewModel GetById(string id);

        void Insert(UserViewModel user);
    }
}
