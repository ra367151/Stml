using AutoMapper;
using Stml.Application.Roles.Dto;
using Stml.Domain.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stml.Application
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Role, RoleDto>()
                .ForMember(dest => dest.Permissions, options => options.MapFrom(src => src.Permissions.Select(p => p.Permission).ToArray()));
        }
    }
}
