using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Domain.Entities;
namespace API.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<User, UserDto>()
            .ReverseMap();
        CreateMap<Rol, RolDto>()
            .ForMember(dest=>dest.Rol, origen=> origen.MapFrom(origen=> origen.Name.ToString()))
            .ReverseMap();
        CreateMap<User,UserAllDto>()
            .ForMember(dest=>dest.Roles, origen=> origen.MapFrom(origen=> origen.Roles))
            .ReverseMap();
    }
}
