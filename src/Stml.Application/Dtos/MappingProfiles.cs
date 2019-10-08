using AutoMapper;
using Stml.Application.Dtos.Inputs;
using Stml.Application.Dtos.Outputs;
using Stml.Domain.Products;
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
        }
    }
}
