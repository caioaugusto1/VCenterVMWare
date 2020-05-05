using AutoMapper;
using System;
using System.Globalization;
using VSphere.Entities;
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
               //.ForMember(x => x.Block, y => y.MapFrom(f => f.Block))
               //.ForMember(x => x.Active, y => y.MapFrom(f => f.Active))
               .ForMember(x => x.Insert, y => y.MapFrom(f => DateTime.Parse(f.Insert.ToString(), new CultureInfo("pt-BR"))));

            CreateMap<VMEntity, VMViewModel>()
               .ForMember(x => x.Id, y => y.MapFrom(f => f.Id))
               .ForMember(x => x.Memory, y => y.MapFrom(f => f.Memory))
               .ForMember(x => x.VM, y => y.MapFrom(f => f.VM))
               .ForMember(x => x.Name, y => y.MapFrom(f => f.Name))
               .ForMember(x => x.CPU, y => y.MapFrom(f => f.CPU))
               .ForMember(x => x.Power, y => y.MapFrom(f => f.Power));
            //.ForMember(x => x.Origem, y => y.MapFrom(f => f.Origem))
            //.ForMember(x => x.Insert, y => y.MapFrom(f => DateTime.Parse(f.Insert.ToString(), new CultureInfo("pt-BR"))));


            CreateMap<HostEntity, HostViewModel>()
                .ForMember(x => x.Id, y => y.MapFrom(f => f.Id))
                .ForMember(x => x.Host, y => y.MapFrom(f => f.Host))
                .ForMember(x => x.Name, y => y.MapFrom(f => f.Name))
                .ForMember(x => x.State, y => y.MapFrom(f => f.State))
                .ForMember(x => x.Power, y => y.MapFrom(f => f.Power))
                .ForMember(x => x.Origem, y => y.MapFrom(f => f.Origem));

            CreateMap<ServerEntity, ServerViewModel>()
                .ForMember(x => x.Id, y => y.MapFrom(f => f.Id))
                .ForMember(x => x.IP, y => y.MapFrom(f => f.IP))
                .ForMember(x => x.UserName, y => y.MapFrom(f => f.UserName))
                .ForMember(x => x.Password, y => y.MapFrom(f => f.Password))
                .ForMember(x => x.Description, y => y.MapFrom(f => f.Description));


            CreateMap<DataStoreEntity, DataStoreViewModel>()
                .ForMember(x => x.Id, y => y.MapFrom(f => f.Id))
                .ForMember(x => x.Name, y => y.MapFrom(f => f.Name))
                .ForMember(x => x.Type, y => y.MapFrom(f => f.Type))
                .ForMember(x => x.FreeSpace, y => y.MapFrom(f => f.FreeSpace))
                .ForMember(x => x.Capacity, y => y.MapFrom(f => f.Capacity))
                .ForMember(x => x.Origem, y => y.MapFrom(f => f.Origem))
                .ForMember(x => x.Insert, y => y.MapFrom(f => DateTime.Parse(f.Insert.ToString(), new CultureInfo("pt-BR"))));
        }
    }
}
