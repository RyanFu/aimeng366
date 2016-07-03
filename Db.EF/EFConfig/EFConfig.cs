using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;

namespace Db.EF
{
    /// <summary>
    /// 没有库时创建
    /// </summary>
    internal class DatabaseInit : CreateDatabaseIfNotExists<MyDbContext>
    {
        /// <summary>
        /// 初始化数据库
        /// </summary>
        /// <param name="context"></param>
        public override void InitializeDatabase(MyDbContext context)
        {
            base.InitializeDatabase(context);
        }
        /// <summary>
        /// 数据库初始化时候执行的初始数据
        /// </summary>
        /// <param name="context"></param>
        protected override void Seed(MyDbContext context)
        {
            DbInitAddData.FirstInit(context);
            try
            {
                context.SaveChanges();
            }
            finally
            {
                context.Configuration.AutoDetectChangesEnabled = true;
                context.Configuration.ValidateOnSaveEnabled = true;
            }

        }
    }
    /// <summary>
    /// 自动升级数据库,只能升级架构
    /// </summary>
    internal class DatabaseInitAutoMigrate : MigrateDatabaseToLatestVersion<MyDbContext, DatabaseInitConfiguration>
    {
        /// <summary>
        /// 初始化数据库
        /// </summary>
        /// <param name="context"></param>
        
        public override void InitializeDatabase(MyDbContext context)
        {
            base.InitializeDatabase(context);
        }
    }
    /// <summary>
    /// 数据库初始化配置  自动升级数据库的时候需要用到
    /// </summary>
    internal sealed class DatabaseInitConfiguration : DbMigrationsConfiguration<MyDbContext>
    {
        public DatabaseInitConfiguration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "Db.EF.MyDbContext";
        }
        protected override void Seed(MyDbContext context)
        {
            DbInitAddData.UpdateInit(context);
        }
    }
}
