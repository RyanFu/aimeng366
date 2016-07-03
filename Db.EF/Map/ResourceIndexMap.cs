using Db.DbModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Db.EF.Map
{
    public class ResourceIndexMap : EntityTypeConfiguration<ResourceIndex>
    {
        public ResourceIndexMap()
        {
            this.ToTable("ResourceIndex");

            this.HasKey(t => t.Id);

            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ResourceId).HasColumnName("ResourceId");
            this.Property(t => t.Name).HasColumnName("Name").HasMaxLength(50);
            this.Property(t => t.ResourceType).HasColumnName("ResourceType");
            this.Property(t => t.ResourceFromsite).HasColumnName("ResourceFromSite");


            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy").HasMaxLength(500);
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy").HasMaxLength(500);
            this.Property(t => t.ModifiedOn).HasColumnName("ModifiedOn");
            this.Property(t => t.Deleted).HasColumnName("Deleted");

        }
    }
}
