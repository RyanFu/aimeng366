using Db.EF.DbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebJobManager.job
{
    /// <summary>
    /// 起点调度器
    /// </summary>
    public static class QiDianMonitorDispatcher
    {
        //当资源费发生变更时  传入资源id
        public static Action<Book> OnResourceChange;
    }
}
