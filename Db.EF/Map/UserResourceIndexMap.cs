using Db.DbModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace Db.EF.Map
{
    public class UserResourceIndexMap : EntityTypeConfiguration<UserResourceIndex>
    {
        public UserResourceIndexMap()
        {
            this.ToTable("UserResourceIndex");

            this.Property(t => t.Id).HasColumnName("Id").HasColumnType("int");
            this.Property(t => t.ResourceIndexId).HasColumnName("ResourceIndexId");
            this.Property(t => t.UserId).HasColumnName("UserId");


            // Relationships
            this.HasRequired(t => t.User)
                .WithMany(t => t.UserResourceIndexs)
                .HasForeignKey(t => t.UserId);
            this.HasRequired(t => t.ResourceIndex)
                .WithMany(t => t.UserResourceIndexs)
                .HasForeignKey(t => t.ResourceIndexId);
        }
    }
}
