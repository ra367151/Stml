using Stml.Application.Dtos.Inputs;
using Stml.Application.Dtos.Outputs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Stml.Application.Services
{
    public interface IProductAppService
    {
        Task<IEnumerable<ProductOutputDto>> GetAllProductsAsync();
        Task CreateNewProductAsync(ProductCreateInputDto product);
    }
}
