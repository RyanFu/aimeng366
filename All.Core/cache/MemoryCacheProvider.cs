using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;

namespace All.Core
{
    /// <summary>
    /// 内存缓存提供器
    /// </summary>
    public static class MemoryCacheProvider
    {
        public static ObjectCache Cache
        {
            get
            {
                return GetCache();
            }
        }
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <returns></returns>
        public static ObjectCache GetCache()
        {
            return MemoryCache.Default;
        }
        public static string DefaultRegionName
        {
            get { return "cache"; }
        }
    }
}
