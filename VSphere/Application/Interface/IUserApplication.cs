using System.Collections.Generic;
using VSphere.Models;

namespace VSphere.Application.Interface
{
    public interface IUserApplication
    {

        UserViewModel GetByUserAndPassword(string user, string password);

        List<UserViewModel> GetAll();

        UserViewModel GetById(string id);

        void Insert(UserViewModel user);
    }
}
