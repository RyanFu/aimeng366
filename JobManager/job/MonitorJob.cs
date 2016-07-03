using All.Core;
using All.Helper;
using ClientWebHelper;
using Quartz;
using SqliteORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using wpfClient.Model;

namespace JobManager.job
{
    internal class MonitorJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            LogHelper.info("请求服务器");
            var data = new HttpService().NutriFunc<SResult<List<BookMessage>>>(AllApiKey.GetMsg, "").GetAwaiter().GetResult();

            if (data != null && data.RState == RState.OK)
            {
                if (data.Resualt != null)
                {
                    using (DbConnection conn = new DbConnection())
                    {
                        foreach (var item in data.Resualt)
                        {
                            item.CreateOn = DateTime.Now;
                            item.Name = item.Name.Trim();
                            item.LastChapter = item.LastChapter.Trim();
                            item.Save();
                        }
                        if (MonitorDispatcher.OnBookMsgLoaded != null)
                        {
                            MonitorDispatcher.OnBookMsgLoaded();
                        }
                    }
                }
            }
        }
    }
}
