using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Stml.Domain.Products;
using Stml.Domain.Roles;
using Stml.Domain.Users;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Stml.Infrastructure.Datas
{
    public class StmlDbContext : IdentityDbContext<User, Role, Guid>
    {
        public StmlDbContext(DbContextOptions<StmlDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
