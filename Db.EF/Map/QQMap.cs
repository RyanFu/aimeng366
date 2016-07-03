//using Db.EF.DbModel;
//using System;
//using System.Collections.Generic;
//using System.Data.Entity.ModelConfiguration;
//using System.Linq;
//using System.Text;

//namespace Db.EF
//{
//    public class QQMap : EntityTypeConfiguration<QQ>
//    {
//        public QQMap()
//        {
//            this.ToTable("QQ");
//            this.HasKey(t => t.Id);

//            this.Property(t => t.Id).HasColumnName("Id");
//            this.Property(t => t.pid).HasColumnName("pid");
//            this.Property(t => t.Name).HasColumnName("Name").HasMaxLength(200);
//            this.Property(t => t.Uin).HasColumnName("Uin").HasMaxLength(200);
//            this.Property(t => t.QQNum).HasColumnName("QQNum").HasMaxLength(200);
//            this.Property(t => t.QQType).HasColumnName("QQType");
//        }
//    }
//}
