using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace All.Core
{
    public class TokenAuthorizeAttribute : System.Web.Http.AuthorizeAttribute
    {
        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            //UserCode
            string userCode = null;
            IEnumerable<string> tempCodes = new List<string>();
            if (actionContext.Request.Headers.TryGetValues("UserCode", out tempCodes))
            {
                userCode = tempCodes.FirstOrDefault();
            }
            //口令
            string token = null;
            IEnumerable<string> tempTokens = new List<string>();
            if (actionContext.Request.Headers.TryGetValues("UserToken", out tempTokens))
            {
                token = tempTokens.FirstOrDefault();
            }

            //客户端版本
            //客户端类型

            if (string.IsNullOrEmpty(userCode) || string.IsNullOrEmpty(token))
            {
                //客户端未发送Code和Token，返回401
                HandleUnauthorizedRequest(actionContext);
                return;
            }

            var cache = MemoryCacheProvider.Cache;

            var cacheToken = cache.Get(userCode+"token");
            if (cacheToken == null || (string)cacheToken != token)
            {
                //Token已失效或与发送的Token不符，返回401
                HandleUnauthorizedRequest(actionContext);
                return;
            }
            //验证通过
            IsAuthorized(actionContext);

            //异步更新缓存，使Token的有效期延长
            System.Threading.ThreadPool.QueueUserWorkItem((o) =>
            {
                try
                {
                    cache.Set(userCode + "token", cacheToken, DateTimeOffset.Now.AddHours(2));
                }
                catch
                {

                }
            });
        }
    }
}
