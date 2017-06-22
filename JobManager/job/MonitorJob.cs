using All.Core;
using All.Helper;
using ClientWebHelper;
using Quartz;
using SqliteORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using wpfClient.Manager;
using wpfClient.Model;

namespace JobManager.job
{
    internal class MonitorJob : IJob
    {
        BookMessageManager service = new BookMessageManager();

        public void Execute(IJobExecutionContext context)
        {
            LogHelper.info("请求服务器");
            var data = new HttpService().NutriFunc<SResult<List<BookMessage>, string>>(AllApiKey.GetMsg, "").GetAwaiter().GetResult();

            if (data != null && data.RState == RState.OK)
            {
                if (data.Resualt != null)
                {
                    foreach (var item in data.Resualt)
                    {
                        var book = service.GetByName(item.Name, item.AuthorName);
                        if (book.LastChapter != item.LastChapter)
                        {
                            book.LastChapter = item.LastChapter;
                            book.LastChapterUrl = item.LastChapterUrl;
                            book.IsRead = false;
                            book.Save();
                        }
                    }
                    //更新服务器更新状态
                    new HttpService().NutriFunc<SResult<List<BookMessage>>>(AllApiKey.SetMsg, new Input<string>() { InputPara = data.Resual2 }).GetAwaiter().GetResult();

                    if (MonitorDispatcher.OnBookMsgLoaded != null)
                    {
                        MonitorDispatcher.OnBookMsgLoaded();
                    }
                }
            }
        }
    }
}
