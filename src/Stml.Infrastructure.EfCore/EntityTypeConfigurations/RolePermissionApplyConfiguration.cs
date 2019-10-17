using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stml.Domain.Roles;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stml.Infrastructure.Datas.EntityTypeConfigurations
{
    public class RolePermissionApplyConfiguration : IEntityTypeConfiguration<RolePermission>
    {
        public void Configure(EntityTypeBuilder<RolePermission> builder)
        {
            builder.ToTable("RolePermissions", "dbo")
                .HasKey(p => p.Id);

            builder.OwnsOne(p => p.Permission);
        }
    }
}
