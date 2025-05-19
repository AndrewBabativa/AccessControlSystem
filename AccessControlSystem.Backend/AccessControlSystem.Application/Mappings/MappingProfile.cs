using AutoMapper;
using AccessControlSystem.Domain.Entities;
using AccessControlSystem.Application.DTOs;

namespace AccessControlSystem.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PermissionType, PermissionTypeDto>().ReverseMap();
            CreateMap<Permission, PermissionDto>().ReverseMap(); 
        }
    }
}
