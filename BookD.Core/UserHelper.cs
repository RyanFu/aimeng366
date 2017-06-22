
//using Db.EF.DbModel;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace BookD.Core
//{
//    public class UserHelper
//    {
//        #region 属性
//        [Import]
//        public static IUserService UserService { get; set; }
//        static UserHelper()
//        {
//            CompositionContainer Container = System.Web.HttpContext.Current.Application["Container"] as CompositionContainer;
//            var export = Container.GetExports(typeof(IUserService), null, null).SingleOrDefault();
//            UserService = export.Value as IUserService;
//        }


//        #endregion

//        public static User GetCurrentUser()
//        {
//            var user = SessionHelper.GetSession<User>(SessionKey.CurrentUser);
//            if (user == null)
//            {
//                user = UserService.Entities.Where(r => r.LoginName == "Guest").ToList().FirstOrDefault();
//                if (user == null)
//                {
//                    user = new User()
//                    {
//                        CreateBy = "system",
//                        CreateTime = DateTime.Now,
//                        LoginName = "Guest",
//                        UserType = Quick.Framework.Common.UserType.Guest,
//                        FullName = "Guest",
//                        RegisterTime = DateTime.Now
//                    };
//                    UserService.Insert(user);
//                }
//                //默认没登陆为游客账号
//                SessionHelper.SetSession(SessionKey.CurrentUser, user);
//            }
//            return user;
//        }
//    }
//}
