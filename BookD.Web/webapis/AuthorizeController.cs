using All.Core;
using All.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace BookD.Web
{

    public class AuthorizeController : BaseController
    {
        /// <summary>
        /// 获取连接Token(口令)
        /// </summary>
        /// <param name="codeAndPwd"></param>
        /// <returns>Token</returns>
        [HttpPost]
        public SResult GToken(Input<string, string> codeAndPwd)
        {
            if (codeAndPwd == null)
            {
                return null;
            }

            var rst = new SResult();

            ///设备编号
            var code = codeAndPwd.InputPara;
            ///连接密码
            var pwd = codeAndPwd.InputPara2;

            if (string.IsNullOrEmpty(code) || string.IsNullOrEmpty(pwd))
            {
                //传入参数为空
                rst.RState = RState.PasswordError;
                rst.Value = "请输入用户名或密码";
                return rst;
            }
            var um = new UserManager();
            var user = um.GetAll().FirstOrDefault(r => r.UserCode == code && r.UserPwd == pwd);

            if (user == null)
            {
                //传入参数为空
                rst.RState = RState.PasswordError;
                rst.Value = "用户名或密码错误";
                return rst;
            }
            var cache = MemoryCacheProvider.Cache;


            //密码正确则通过成Token并、缓存，返回给客户端
            var token = "";
            
            try
            {
                var oldtoken = cache.Get(code + "token");
                if (oldtoken == null || oldtoken == "")
                {
                    token = Guid.NewGuid().ToString();
                    ///将Token存入缓存.2小时过期
                    cache.Set(code + "token", token, DateTimeOffset.Now.AddHours(2));
                }
                else
                {
                    token = oldtoken.ToString();
                }
            }
            catch (Exception)
            {
            }
            rst.RState = RState.OK;
            rst.Value = token;
            return rst;
        }
    }
}