using All.Core;
using Db.DbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Threading;
using All.Helper;
using Db.EF;
using System.Text.RegularExpressions;
using Sgml;
using System.IO;
using System.Xml;
using Db.EF.DbModel;
using All.Repo;

namespace ResourceMonitor
{
    public static class Monitor
    {
        //当资源费发生变更时  传入资源id
        public static Action<Book> OnResourceChange;
        public static HttpHelper BookHttpHelper = new HttpHelper();
        public static List<Book> QiDianResource = null;
        /// <summary>
        /// 监视起点
        /// </summary>
        public static void MonitorQiDian()
        {
            Task t = new Task(zhongjian);
            t.Start();
        }

        public static void zhongjian()
        {
            while (true)
            {
                var startTime = DateTime.Now;
                Task t = new Task(QiDianRequest);
                t.Start();
                t.Wait();
                //半个小时寻找一次
                if (startTime.AddHours(0.125) > DateTime.Now)
                {
                    TimeSpan ts1 = new TimeSpan(DateTime.Now.Ticks);
                    TimeSpan ts2 = new TimeSpan(startTime.Ticks);
                    TimeSpan ts = ts1.Subtract(ts2).Duration();
                    var tmp = 450000 - ts.TotalMilliseconds;
                    Thread.Sleep((int)tmp);
                }
            }
        }

        private static void QiDianRequest()
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
                               if (OnResourceChange != null)
                               {
                                   LogHelper.info(newResource.ResourceIndex.Name + "更新了 " + DateTime.Now);
                                   OnResourceChange(newResource);
                               }

                           }
                       });
                    task.Start();
                }
                catch (Exception e)
                {
                    LogHelper.error(e.Message);
                }
                Thread.Sleep(100);
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
