using Db.DbModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace Db.EF.Map
{
    public class UpdateInfoMap : EntityTypeConfiguration<UpdateInfo>
    {
        public UpdateInfoMap()
        {
            this.ToTable("UpdateInfo");

            this.HasKey(t => t.Id);

            this.Property(t => t.Id).HasColumnName("");
            this.Property(t => t.AppName).HasColumnName("AppName");
            this.Property(t => t.AppVersion).HasColumnName("AppVersion");
            this.Property(t => t.Desc).HasColumnName("Desc");

            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy").HasMaxLength(500);
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy").HasMaxLength(500);
            this.Property(t => t.ModifiedOn).HasColumnName("ModifiedOn");
            this.Property(t => t.Deleted).HasColumnName("Deleted");
        }
    }
}
