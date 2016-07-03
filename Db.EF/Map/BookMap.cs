using Db.EF.DbModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace Db.EF
{
    public class BookMap : EntityTypeConfiguration<Book>
    {
        public BookMap()
        {
            this.ToTable("Book");

            this.HasKey(t => t.ResourceIndexId);

            this.Property(t => t.ResourceIndexId).HasColumnName("ResourceIndexId")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.AuthorName).HasColumnName("AuthorName").HasMaxLength(200);
            this.Property(t => t.ImgUrl).HasColumnName("ImgUrl").HasMaxLength(200);
            this.Property(t => t.Description).HasColumnName("Description").HasMaxLength(500);
            this.Property(t => t.LastChapter).HasColumnName("LastChapter").HasMaxLength(500);
            this.Property(t => t.LastChapterUrl).HasColumnName("LastChapterUrl").HasMaxLength(500);

            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy").HasMaxLength(500);
            this.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
            this.Property(t => t.ModifiedBy).HasColumnName("ModifiedBy").HasMaxLength(500);
            this.Property(t => t.ModifiedOn).HasColumnName("ModifiedOn");
            this.Property(t => t.Deleted).HasColumnName("Deleted");


            this.HasRequired(r => r.ResourceIndex).WithOptional();
        }
    }
}
