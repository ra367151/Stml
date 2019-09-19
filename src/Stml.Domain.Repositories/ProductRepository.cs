using Microsoft.EntityFrameworkCore;
using Stml.Domain.Entities;
using Stml.Infrastructure.Datas;
using Stml.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stml.Domain.Repositories
{
    public class ProductRepository : EfCoreRepository<StmlDbContext, Product>, IProductRepository
    {
        public ProductRepository(StmlDbContext dbContext) : base(dbContext)
        {
        }
    }
}
