using All.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClientWebHelper
{
    public class AllConfigKeys
    {
        /// <summary>
        /// 用户账号
        /// </summary>
        public const string UserCode = "UserCode";


        /// <summary>
        /// 口令
        /// </summary>
        public const string UserToken = "UserToken";
        
        /// <summary>
        /// 当前版本
        /// </summary>
        public const string UserVersion = "UserVersion";

        /// <summary>
        /// 主机url 
        /// </summary>
        public const string ServiceUrl = "ServiceUrl";
        /// <summary>
        /// 记住密码 
        /// </summary>
        public const string SavePwd = "SavePwd";

        public const ClientType UserClientType = ClientType.wpfClient;
    }
}
