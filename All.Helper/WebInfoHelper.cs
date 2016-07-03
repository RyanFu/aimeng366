using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace All.Helper
{
    /// <summary>
    /// web相关请求相关信息
    /// </summary>
    public class WebInfoHelper
    {
        /// <summary>
        /// 客户端主机名:
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        public static string GetClientName(HttpRequest Request)
        {
            return Request.ServerVariables.Get("Remote_Host").ToString();
        }

        /// <summary>
        /// 客户端Ip:
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        public static string GetClientIp(HttpRequest Request)
        {
            return Request.ServerVariables.Get("Remote_Addr").ToString(); 
        }

        /// <summary>
        /// 客户端浏览器版本号
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        public static int GetBrowserVersion(HttpRequest Request)
        {
            return Request.Browser.MajorVersion; 
        }

        
        /// <summary>
        /// 客户端系统版本号
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        public static string GetClientOSVersion(HttpRequest Request)
        {
            return Request.Browser.Platform; 
        }
        
        


    }
}
