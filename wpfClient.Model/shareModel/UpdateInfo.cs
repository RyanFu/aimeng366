using All.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace wpfClient.Model
{
    /// <summary>
    /// 升级信息的具体包装
    /// </summary>
    public class UpdateInfo : SResult
    {
        public string AppName { get; set; }

        /// <summary>
        /// 应用程序版本
        /// </summary>
        public string AppVersion { get; set; }

        public Version Version
        {
            get
            {
                return new Version(AppVersion);
            }
            set
            {
                AppVersion = value.ToString();
            }
        }

        public Guid MD5
        {
            get;
            set;
        }

        private string _desc;
        /// <summary>
        /// 更新描述
        /// </summary>
        public string Desc
        {
            get
            {
                return _desc;
            }
            set
            {
                if (value != null)
                {
                    _desc = string.Join(Environment.NewLine, value.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries));
                }
            }
        }
    }
}
