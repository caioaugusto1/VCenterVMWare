using AutoMapper;
using System;
using System.Globalization;
using VCenter.Entities;
using VSphere.Models;

namespace VSphere.AutoMapper
{
    public class MappingsProfile : Profile
    {
        public MappingsProfile()
        {
            CreateMap<UserEntity, UserViewModel>()
               .ForMember(x => x.Id, y => y.MapFrom(f => f.Id))
               .ForMember(x => x.FullName, y => y.MapFrom(f => f.FullName))
               .ForMember(x => x.Email, y => y.MapFrom(f => f.Email))
               .ForMember(x => x.Password, y => y.MapFrom(f => f.Password))
               .ForMember(x => x.Block, y => y.MapFrom(f => f.Block))
               .ForMember(x => x.Active, y => y.MapFrom(f => f.Active))
               .ForMember(x => x.Insert, y => y.MapFrom(f => DateTime.Parse(f.Insert.ToString(), new CultureInfo("pt-BR"))));
        }
    }
}
