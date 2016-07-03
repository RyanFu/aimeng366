using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookD.Core
{
    /// <summary>
    /// 分页请求基类
    /// </summary>
    public class RequestPageBase
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
    }
}
