using Db.EF.Map;
using MySql.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace Db.EF
{
    //[DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class MyDbContext : DbContext
    {
        static bool isInit;
        public MyDbContext()
            : base("MyDbContext")
        {
            if (!isInit)
            {
                //如果数据库不存在就删除数据库  如果数据库存在就自动升级结构  如果升级失败  请删除数据库
                if (!Database.Exists("MyDbContext"))
                {
                    Database.SetInitializer<MyDbContext>(new DatabaseInit());
                }
                else
                {
                    Database.SetInitializer<MyDbContext>(new DatabaseInitAutoMigrate());//先使用自动合并
                }
                isInit = true;
            }
        }

        /// <summary>
        /// 注册所有模型
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            ModelMapRegister.RegisterModel(modelBuilder);
        }
    }
}
