using Db.EF.DbModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace Db.EF.Map
{
    internal class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            this.ToTable("User");

            this.HasKey(t => t.Id);

            this.Property(t => t.Id).HasColumnName("Id").HasColumnType("int");
            this.Property(t => t.Email).HasColumnName("Email").HasMaxLength(200);
            this.Property(t => t.Phone).HasColumnName("Phone").HasMaxLength(200);
            this.Property(t => t.QQ).HasColumnName("QQ").HasMaxLength(200);
            this.Property(t => t.UserCode).HasColumnName("UserCode").HasMaxLength(200);
            this.Property(t => t.UserName).HasColumnName("UserName").HasMaxLength(200);
            this.Property(t => t.UserPwd).HasColumnName("UserPwd").HasMaxLength(20);
            this.Property(t => t.UserType).HasColumnName("UserType");


            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy").HasMaxLength(500);
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy").HasMaxLength(500);
            this.Property(t => t.ModifiedOn).HasColumnName("ModifiedOn");
            this.Property(t => t.Deleted).HasColumnName("Deleted");

        }
    }
}
