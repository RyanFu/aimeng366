using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Db.EF
{
    /// <summary>
    /// 数据库初始化的时候添加的数据
    /// </summary>
    public static class DbInitAddData
    {
        public static Action<MyDbContext> FirstInitAction;
        public static Action<MyDbContext> UpdateInitAction;
        /// <summary>
        /// 数据库创建的时候
        /// </summary>
        /// <param name="context"></param>
        public static void FirstInit(MyDbContext context)
        {
            if (FirstInitAction!=null)
            {
                FirstInitAction(context);
            }
        }

        /// <summary>
        /// 数据库结构升级的时候
        /// </summary>
        /// <param name="context"></param>
        public static void UpdateInit(MyDbContext context)
        {
            if (UpdateInitAction!=null)
            {
                UpdateInitAction(context);
            }
        }

    }
}
