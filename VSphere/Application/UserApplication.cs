using System;
using System.Collections.Generic;
using VCenter.Models;
using VCenter.Repositories.Interfaces;
using VCenter.Services;
using VCenter.Services.Inteface;
using VCenterVMWare.Application.Inteface;

namespace VCenterVMWare.Application
{
    public class UserApplication : IUserApplication
    {

        private readonly IUserRepository _userRepository;
        public UserApplication(IUserRepository userRepository)
        {
            _userRepository = userRepository;          
        }

        public List<UserEntity> GetAll()
        {
            return _userRepository.GetAll();
        }

        public UserEntity GetById(string id)
        {
            return _userRepository.GetById(id);
        }

        public List<UserEntity> GetUserAndPassword(string user, string password)
        {
            throw new NotImplementedException();
        }

        public void Insert(UserEntity user)
        {
            _userRepository.Insert(user);
        }
    }
}
