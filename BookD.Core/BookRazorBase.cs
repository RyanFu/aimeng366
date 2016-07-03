using Db.DbModel;
using Db.EF.DbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace BookD.Core
{
    /// <summary>
    /// razor的自定义基类
    /// </summary>
    public class BookRazorBase<T> : WebViewPage<T>
    {
        public UserType UserType
        {
            get
            {
                var user = System.Web.HttpContext.Current.Session["User"] as User;
                if (user != null)
                {
                    return user.UserType;
                }
                else
                {
                    return UserType.everyOne;
                }
            }
        }
        public User User
        {
            get
            {
                return System.Web.HttpContext.Current.Session["User"] as User;
            }
        }



        /// <summary>
        /// 把url还原  不是
        /// </summary>
        /// <returns></returns>
        public string MyDeCodeUrl(string url)
        {
            if (url == null)
            {
                return "";
            }
            var rst = url.Replace("AEAE", "://").Replace("ACECE", ".").Replace("XAPZ", "/").Replace("ASPEW", ",");
            return rst;
        }

        /// <summary>
        /// 给url编码 让它可以在mvc中传递
        /// </summary>
        /// <returns></returns>
        public string MyCodeUrl(string url)
        {
            if (url == null)
            {
                return "";
            }
            var rst = url.Replace("://", "AEAE").Replace(".", "ACECE").Replace("/", "XAPZ").Replace(",", "ASPEW");
            return rst;
        }


        public override void Execute()
        {
        }
    }
}
