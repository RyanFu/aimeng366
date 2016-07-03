using Db.DbModel;
using Db.EF;
using Db.EF.DbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using All.Core;
using All.Repo;
using WebJobManager;
using WebJobManager.job;

namespace BookD.Web
{
    /// <summary>
    /// 监视处理器
    /// </summary>
    public class MonitorHandler
    {
        public MonitorHandler()
        {
            QiDianMonitorDispatcher.OnResourceChange -= Resource_Change;
            QiDianMonitorDispatcher.OnResourceChange += Resource_Change;
            JobManager.Instance.StartJobServer();
        }

        private void Resource_Change(Book obj)
        {
            var context = WebRepoFactory.CreateRepo<UserResourceIndex>();
            var urs = context.Where(r => r.ResourceIndexId == obj.ResourceIndex.Id).Select(r => r.User).ToList();
            if (urs.Count() > 0)
            {
                foreach (var item in urs)
                {
                    var temp = new BookUserMsg()
                    {
                        UserMsg = new UserMsg()
                        {
                            User = item,
                            Msg = "更新提示"
                        },
                        LastChapterUrl = obj.LastChapterUrl,
                        LastChapter = obj.LastChapter,
                        BookId = obj.ResourceIndexId
                    };
                    WebRepoFactory.CreateRepo<BookUserMsg>(context.dbContext).Insert(temp);
                }
                context.Save();
            }
        }
    }
}
