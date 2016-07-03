using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace All.ConfigHelper
{
    public class ConfigHelper
    {
        /// <summary>
        /// 读取配置
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetStringConfig(string key)
        {
            return ConfigurationManager.AppSettings.Get(key);
        }
        /// <summary>
        /// 写入配置
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static void SetStringConfig(string key, string value)
        {
            ConfigurationManager.AppSettings.Set(key, value);
        }
    }
}
