using All.Core;
using Db.EF;
using Db.EF.DbModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace All.Repo
{
    /// <summary>
    /// 使用工厂 确保 dbcontext 生命周期为一次请求
    /// </summary>
    public static class WebRepoFactory
    {
        public static Repo<T> CreateRepo<T>(MyDbContext dbcontext = null)
            where T : class,IBaseData, new()
        {
            if (dbcontext == null)
            {
                dbcontext = new MyDbContext();
            }
            var repo = new Repo<T>();
            repo.dbContext = dbcontext;
            if (System.Web.HttpContext.Current != null && System.Web.HttpContext.Current.Session != null)
            {
                repo.User = System.Web.HttpContext.Current.Session["User"] as User;
            }
            return repo;
        }
    }
}
