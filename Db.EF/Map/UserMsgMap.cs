using Db.DbModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace Db.EF.Map
{
    public class UserMsgMap : EntityTypeConfiguration<UserMsg>
    {
        public UserMsgMap()
        {
            this.ToTable("UserMsg");

            this.HasKey(t => t.Id);

            this.Property(t => t.Id).HasColumnName("Id").HasColumnType("int");
            this.Property(t => t.Msg).HasColumnName("Msg").HasMaxLength(200);;
            this.Property(t => t.IsRead).HasColumnName("IsRead");
            this.Property(t => t.ToUserId).HasColumnName("ToUserId");

            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy").HasMaxLength(500);
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy").HasMaxLength(500);
            this.Property(t => t.ModifiedOn).HasColumnName("ModifiedOn");
            this.Property(t => t.Deleted).HasColumnName("Deleted");



            this.HasRequired(r => r.User).WithMany().HasForeignKey(r=>r.ToUserId);

        }
    }
}
