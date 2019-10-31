using AutoMapper;
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
        }
    }
}
