using System.Collections.Generic;
using VCenter.Models;

namespace VCenterVMWare.Application.Inteface
{
    public interface IUserApplication
    {
        List<UserEntity> GetUserAndPassword(string user, string password);

        List<UserEntity> GetAll();

        UserEntity GetById(string id);

        void Insert(UserEntity user);
    }
}
