using AutoMapper;
using System.Collections.Generic;
using VSphere.Application.Interface;
using VSphere.Entities;
using VSphere.Models;
using VSphere.Repositories.Interfaces;
using VSphere.Utils;

namespace VSphere.Application
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
            return _mapper.Map<List<UserViewModel>>(_userRepository.GetAll());
        }

        public UserViewModel GetById(string id)
        {
            return _mapper.Map<UserViewModel>(_userRepository.GetById(id));
        }

        public UserViewModel GetByUserAndPassword(string user, string password)
        {
            return _mapper.Map<UserViewModel>(_userRepository.GetByUserAndPassword(user, Crypto.EncryptMd5(password)));
        }

        public void Insert(UserViewModel user)
        {
            user.Password = Crypto.EncryptMd5(user.Password);

            var entity = new UserEntity(user.FullName, user.Email, user.Password);

            _userRepository.Insert(entity);
        }
    }
}
