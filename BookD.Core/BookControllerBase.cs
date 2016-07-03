using All.Core;
using Db.DbModel;
using Db.EF.DbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace BookD.Core
{
    public class BookControllerBase : Controller
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

        public JsonResult CreateJson(ResultCode code, string msg)
        {
            var rst = new WebResponseBaseModel();
            rst.Code = code;
            rst.Msg = msg;
            return Json(rst,JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 把url还原  不是
        /// </summary>
        /// <returns></returns>
        public string MyDeCodeUrl(string url)
        {
            if (url==null)
            {
                return "";
            }
            var rst = url.Replace("AEAE", "://").Replace("ACECE", ".").Replace("XAPZ", "/").Replace("ASPEW",",");
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
    }
}