using AutoMapper;
using Stml.Application.Roles.Dto;
using Stml.Application.Users.Dto;
using Stml.Domain.Authorizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stml.Application
{
    public class ApplicationMapProfile : Profile
    {
        public ApplicationMapProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Roles, options => options.MapFrom(src => src.UserRoles.Select(ur => ur.Role)));

            CreateMap<Role, RoleDto>()
                .ForMember(dest => dest.Permissions, options => options.MapFrom(src => src.Permissions.Select(p => p.Permission).ToArray()));
        }
    }
}
