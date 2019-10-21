using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Stml.Application.Dtos.Inputs;
using Stml.Application.Dtos.Outputs;
using Stml.Domain.Products;
using Stml.Domain.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stml.Application.Dtos
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<ProductCreateInput, Product>();

            CreateMap<User, UserDto>()
                .ForMember(dest => dest.CreationTime, m => m.MapFrom(source => source.CreationTime.ToString("yyyy-MM-dd")));
        }
    }
}
