using AutoMapper;
using System.Collections.Generic;
using VCenter.Entities;
using VCenter.Repositories.Interfaces;
using VCenter.Utils;
using VCenterVMWare.Application.Inteface;
using VSphere.Models;

namespace VCenterVMWare.Application
{
    public class UserApplication : IUserApplication
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserApplication(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public List<UserViewModel> GetAll()
        {
            return _mapper.Map<List<UserEntity>, List<UserViewModel>>(_userRepository.GetAll());
        }

        public UserViewModel GetById(string id)
        {
            return _mapper.Map<UserEntity, UserViewModel>(_userRepository.GetById(id));
        }

        public UserViewModel GetByUserAndPassword(string user, string password)
        {
            return _mapper.Map<UserEntity, UserViewModel>(_userRepository.GetByUserAndPassword(user, Crypto.CryptoMd5(password)));
        }

        public void Insert(UserViewModel user)
        {
            user.Password = Crypto.CryptoMd5(user.Password);

            var entity = new UserEntity(user.FullName, user.Email, user.Password);

            _userRepository.Insert(entity);
        }
    }
}
