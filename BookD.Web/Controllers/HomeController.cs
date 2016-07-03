using All.Core;
using All.Helper;
using All.Manager;
using BookD.Bill;
using BookD.Core;
using Db.EF.DbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookD.Web.Controllers
{
    public class HomeController : BookControllerBase
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public string Login(string username, string password, string verCode)
        {
            var vercode = Session[BookD.Core.SessionName.BookDLogin];
            if (vercode != null)
            {
                if (vercode.ToString() != verCode)
                {
                    var msg = "<script>window.parent.ShowMsg('登录失败','验证码输入错误！');window.parent.showVerCode();</script>";
                    return msg;
                }
            }
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                var msg = "<script>window.parent.ShowMsg('登录失败','请填写用户名或密码！');window.parent.showVerCode();</script>";
                return msg;
            }

            var um = new UserManager();
            var user = um.GetAll().FirstOrDefault(r => r.UserCode == username && r.UserPwd == password);
            if (user != null)
            {
                Session.Remove(BookD.Core.SessionName.BookDLogin.ToString());
                Session.Add(SessionName.User.ToString(), user);
                //成功 返回登录页面
                var msg = "<script>parent.window.location.href=parent.window.loginRefer</script>";
                return msg;
            }
            else
            {
                var msg = "<script>window.parent.ShowMsg('登录失败','用户名或密码错误！');window.parent.showVerCode();</script>";
                return msg;
            }
        }
        [HttpGet]
        public ActionResult Register()
        {
            Session.Add(BookD.Core.SessionName.BookDRegister.ToString(), "vercode");
            return View();
        }
        [HttpPost]
        public string Register(string username, string password, string verCode)
        {
            var vercode = Session[BookD.Core.SessionName.BookDRegister.ToString()];
            if (vercode != null)
            {
                if (vercode.ToString() != verCode)
                {
                    var msg = "<script>window.parent.ShowMsg('注册失败','验证码输入错误！')</script>";
                    return msg;
                }
            }
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                var msg = "<script>window.parent.ShowMsg('注册失败','请填写用户名或密码！')</script>";
                return msg;
            }
            var um = new UserManager();
            var user = new Db.EF.DbModel.User()
            {
                UserPwd = password,
                UserCode = username,
                UserType = Db.DbModel.UserType.vipOne
            };
            try
            {
                var temp = um.Create(user, true);
                if (temp != null)
                {
                    var msg = "<script>window.parent.ShowMsg('注册失败','" + temp.ErrorMessage + "！')</script>";
                    return msg;
                }
                else
                {
                    Session.Remove(BookD.Core.SessionName.BookDRegister);
                    //成功 返回登录页面
                    var msg = "<script>parent.window.location.href='/home/login'</script>";
                    return msg;
                }
            }
            catch (Exception e)
            {
                LogHelper.error(e.Message);
                var msg = "<script>window.parent.ShowMsg('注册失败','" + e.Message + "！')</script>";
                return msg;
            }
        }
        [HttpGet]
        public ActionResult LoginOut()
        {
            //清除状态
            Session.Remove(SessionName.User);
            return Redirect(Request.UrlReferrer.ToString());
        }
        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetVerCode(string verName)
        {
            var verCode = "";
            var imgByte = VerCodeHelper.CreateValidateGraphic(out verCode);
            Session.Add(verName.ToString(), verCode);
            return File(imgByte, "image/jpeg");
        }
        [HttpGet]
        public ActionResult ShowUrl(string url)
        {
            ViewData["url"] = MyDeCodeUrl(url);
            return View();
        }
        /// <summary>
        /// 加入提醒
        /// </summary>
        /// <returns></returns>
        public ActionResult AddRemind(string resourceId, ResourceFromSite site)
        {
            var service = new SearchModelManager();
            var rst = service.AddRemindBook(resourceId, site, User);
            if (rst != null)
            {
                return CreateJson(ResultCode.失败, rst.ErrorMessage);
                
            }
            else
            {
                return CreateJson(ResultCode.成功, "");
            }
        }
    }
}
