using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ResourceMonitor
{
    public class MonitorConfig
    {
        /// <summary>
        /// 加载所有的监视器
        /// </summary>
        public static void LoadMonitor()
        {
            Monitor.MonitorQiDian();
        }
    }
}
