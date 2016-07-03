using Db.DbModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace Db.EF.Map
{
    public class BookUserMsgMap : EntityTypeConfiguration<BookUserMsg>
    {
        public BookUserMsgMap()
        {
            this.ToTable("BookUserMsg");

            this.HasKey(t => t.UserMsgId);

            this.Property(t => t.UserMsgId).HasColumnName("UserMsgId")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.BookId).HasColumnName("BookId");
            this.Property(t => t.UserMsgId).HasColumnName("UserMsgId");
            this.Property(t => t.LastChapter).HasColumnName("LastChapter");
            this.Property(t => t.LastChapterUrl).HasColumnName("LastChapterUrl");


            this.HasRequired(r => r.UserMsg).WithOptional();
            this.HasRequired(r => r.Book).WithMany().HasForeignKey(r => r.BookId);
        }
    }
}
