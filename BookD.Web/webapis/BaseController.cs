using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace BookD.Web
{
    public class BaseController : ApiController
    {
        string _token;
        string _code { get; set; }
        /// <summary>
        /// 连接口令
        /// </summary>
        public string Token
        {
            get
            {
                if (_token == null && Request != null)
                {
                    IEnumerable<string> tempTokens = new List<string>();
                    Request.Headers.TryGetValues("UserToken", out tempTokens);
                    _token = tempTokens.FirstOrDefault();
                }
                return _token;
            }
        }

        /// <summary>
        /// 用户账号
        /// </summary>
        public string Code
        {
            get
            {
                if (_code == null && Request != null)
                {

                    IEnumerable<string> tempCodes = new List<string>();
                    Request.Headers.TryGetValues("UserCode", out tempCodes);
                    _code = tempCodes.FirstOrDefault();

                }
                return _code;
            }
        }

        
    }
}