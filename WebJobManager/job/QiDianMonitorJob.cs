using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quartz;
using All.Repo;
using Db.EF.DbModel;
using All.Core;
using All.Helper;
using System.Threading.Tasks;
using System.Data.Entity;

namespace WebJobManager.job
{
    public class QiDianMonitorJob : IJob
    {
        public static HttpHelper BookHttpHelper = new HttpHelper();
        public static List<Book> QiDianResource = null;

        public void Execute(IJobExecutionContext context)
        {
            LoadQiDianResource();
            for (int i = 0; i < QiDianResource.Count; i++)
            {
                try
                {
                    var tempResource = QiDianResource[i];
                    Task task = new Task(() =>
                       {
                           string responseString = BookHttpHelper.GetHtml("http://www.qidian.com/book/" + tempResource.ResourceIndex.ResourceId + ".aspx", HttpRefer.QiDian);
                           var data = HTMLtoXMLHelper.GetWellFormedHTML(responseString, "/html//div[@id='readV']");
                           //最新章节
                           var chapter = HTMLtoXMLHelper.PartHtml(data, "/div/div[@class='title']/h3/a//strong");
                           if (chapter.Trim() != tempResource.LastChapter.Trim())
                           {
                               var tempContext = WebRepoFactory.CreateRepo<Book>();
                               var newResource = tempContext.Get(tempResource.ResourceIndexId);
                               newResource.LastChapter = chapter;
                               newResource.LastChapterUrl = HTMLtoXMLHelper.PartHtml(data, "/div/div[@class='title']/h3/a/@href");
                               tempContext.Save();
                               if (QiDianMonitorDispatcher.OnResourceChange != null)
                               {
                                   LogHelper.info(newResource.ResourceIndex.Name + "更新了 " + DateTime.Now);
                                   QiDianMonitorDispatcher.OnResourceChange(newResource);
                               }
                           }
                       });
                    task.Start();
                }
                catch (Exception e)
                {
                    LogHelper.error(e.Message);
                }
            }
        }

        private static void LoadQiDianResource()
        {
            All.Repo.Repo<Book> DbContext = WebRepoFactory.CreateRepo<Book>();
            var service = DbContext.Where(r => r.ResourceIndex.SubscribeNum > 0 && r.ResourceIndex.ResourceFromsite == Db.EF.DbModel.ResourceFromSite.起点).Include(r => r.ResourceIndex);
            var data = service.ToList();
            QiDianResource = data;
            MemoryCacheProvider.Cache.Add("QiDianResource", data, DateTimeOffset.Now.AddHours(1));
        }
    }
}
