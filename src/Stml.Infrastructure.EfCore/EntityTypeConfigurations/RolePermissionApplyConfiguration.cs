//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using Stml.Domain.Authorizations;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace Stml.EntityFrameworkCore.EntityTypeConfigurations
//{
//    public class RolePermissionApplyConfiguration : IEntityTypeConfiguration<RolePermission>
//    {
//        public void Configure(EntityTypeBuilder<RolePermission> builder)
//        {
//            builder.ToTable("RolePermissions", "dbo")
//                .HasKey(p => p.Id);

//            builder.HasOne(rp => rp.Role)
//                .WithMany(r => r.RolePermissions)
//                .HasForeignKey(rp => rp.RoleId)
//                .IsRequired();

//            builder.OwnsOne(p => p.Permission).WithOwner();
//        }
//    }
//}
