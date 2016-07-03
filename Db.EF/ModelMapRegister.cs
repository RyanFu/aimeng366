using Db.EF.Map;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace Db.EF
{
    /// <summary>
    /// 模型映射注册
    /// </summary>
    public static class ModelMapRegister
    {
        public static Action<DbModelBuilder> RegisterModelAction;
        internal static void RegisterModel(DbModelBuilder modelBuilder)
        {
            //if (RegisterModelAction != null)
            //{
            //    RegisterModelAction(modelBuilder);
            //}
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new UserResourceIndexMap());
            modelBuilder.Configurations.Add(new ResourceIndexMap());
            modelBuilder.Configurations.Add(new BookMap());
            modelBuilder.Configurations.Add(new UserMsgMap());
            modelBuilder.Configurations.Add(new BookUserMsgMap());
            modelBuilder.Configurations.Add(new UpdateInfoMap());

            //modelBuilder.Configurations.Add(new QQMap());
        }
    }
}
