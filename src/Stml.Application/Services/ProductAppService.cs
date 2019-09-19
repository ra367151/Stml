using Stml.Application.Dtos.Inputs;
using Stml.Application.Dtos.Outputs;
using Stml.Domain.Entities;
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

        public ProductAppService(IProductRepository productRepository, IEfCoreUnitOfWork<StmlDbContext> stmlUnitOfWork)
        {
            _productRepository = productRepository;
            _stmlUnitOfWork = stmlUnitOfWork;
        }

        public async Task CreateNewProductAsync(ProductCreateInputDto product)
        {
            var entity = new Product(product.Name, product.Price);
            _productRepository.Add(entity);
            await _stmlUnitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<ProductOutputDto>> GetAllProductsAsync()
        {
            return (await _productRepository.GetAllAsync()).Select(p => new ProductOutputDto(p.Id, p.Name, p.Price));
        }
    }
}
