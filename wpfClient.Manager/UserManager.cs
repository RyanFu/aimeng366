using SqliteORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using wpfClient.Model;

namespace wpfClient.Manager
{
    public class UserManager
    {
        public User GetUser(Where where)
        {
            using (TableAdapter<User> adapter = TableAdapter<User>.Open())
            {
                return adapter.Select().Where(where).FirstOrDefault();
            }
        }
        public void Insert(User user)
        {
            user.Save();
        }
        /// <summary>
        /// 获取记住密码的用户
        /// </summary>
        /// <returns></returns>
        public User GetRememberPwdUser()
        {
            using (TableAdapter<User> adapter = TableAdapter<User>.Open())
            {
                return adapter.Select().Where(Where.Equal("IsReadPwd", true)).FirstOrDefault();
            }
        }
        /// <summary>
        /// 设置记住密码的用户
        /// </summary>
        /// <param name="user"></param>
        public void SetRememberPwdUser(string code,string pwd, bool state)
        {
            using (TableAdapter<User> adapter = TableAdapter<User>.Open())
            {
                var user = adapter.Select().Where(Where.Equal("Code", code)).FirstOrDefault();
                if (user != null)
                {
                    adapter.ExecuteSql("update User set IsReadPwd = 'false'");
                    user.IsReadPwd = state;
                    user.Pwd = pwd;
                    user.Save();
                }
                else
                {
                    user = new User()
                    {
                        Code = code,
                        CreateOn = DateTime.Now,
                        IsReadPwd = state,
                        Pwd = pwd
                    };
                    user.Save();
                }
            }
        }
    }
}
