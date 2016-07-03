using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace All.Core
{
    /// <summary>
    /// web请求操作 返回操作结果基类
    /// </summary>
    public class WebResponseBaseModel
    {
        public ResultCode Code { get; set; }
        public string Msg { get; set; }
    }

    public enum ResultCode
    {
        失败 = 0,
        成功 = 1
    }
}
