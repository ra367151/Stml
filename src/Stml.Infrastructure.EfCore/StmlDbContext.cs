using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Stml.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Stml.Infrastructure.Datas
{
    public class StmlDbContext : DbContext
    {
        public StmlDbContext([NotNull] DbContextOptions<StmlDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
