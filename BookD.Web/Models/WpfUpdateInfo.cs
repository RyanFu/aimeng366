using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookD.Web
{
    public class WpfUpdateInfo
    {
        public string AppName { get; set; }
        /// <summary>
        /// 应用程序版本
        /// </summary>
        public Version AppVersion { get; set; }
        /// <summary>
        /// 更新描述
        /// </summary>
        public string Desc { get; set; }
    }
}