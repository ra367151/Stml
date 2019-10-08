using AutoMapper;
using Stml.Application.Dtos.Inputs;
using Stml.Application.Dtos.Outputs;
using Stml.Domain.Products;
using Stml.Domain.Repositories;
using Stml.Infrastructure.Datas;
using Stml.Infrastructure.Uow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stml.Application.Services
{
    public class ProductAppService : IProductAppService
    {
        private readonly IProductRepository _productRepository;
        private readonly IEfCoreUnitOfWork<StmlDbContext> _stmlUnitOfWork;
        private readonly IMapper _mapper;

        public ProductAppService(IProductRepository productRepository
            , IEfCoreUnitOfWork<StmlDbContext> stmlUnitOfWork
            , IMapper mapper)
        {
            _productRepository = productRepository;
            _stmlUnitOfWork = stmlUnitOfWork;
            _mapper = mapper;
        }

        public async Task CreateNewProductAsync(ProductCreateInput product)
        {
            _productRepository.Add(_mapper.Map<Product>(product));
            await _stmlUnitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            return _mapper.Map<IEnumerable<ProductDto>>(await _productRepository.GetAllAsync());
        }
    }
}
