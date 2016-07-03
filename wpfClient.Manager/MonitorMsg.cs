//using All.Core;
//using All.Helper;
//using ClientWebHelper;
//using SqliteORM;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;
//using wpfClient.Model;

//namespace wpfClient.Manager
//{
//    /// <summary>
//    /// 消息监视器
//    /// </summary>
//    public static class MonitorMsg
//    {
//        public static Action OnBookMsgLoaded;
//        public static void RegisterMonitor()
//        {
//            Task task = new Task(() => 
//            {
//                LogHelper.info("请求服务器");
//                var data = new HttpService().NutriFunc<SResult<List<BookMessage>>>(AllApiKey.GetMsg, "").GetAwaiter().GetResult();
//                if (data!=null&&data.RState == RState.OK)
//                {
//                    if (data.Resualt!=null)
//                    {
//                        using (DbConnection conn = new DbConnection())
//                        {
//                            foreach (var item in data.Resualt)
//                            {
//                                item.CreateOn = DateTime.Now;
//                                item.Name = item.Name.Trim();
//                                item.LastChapter = item.LastChapter.Trim();
//                                item.Save();
//                            }
//                            if (OnBookMsgLoaded!=null)
//                            {
//                                OnBookMsgLoaded();
//                            }
//                        }
//                    }
//                }
//                //休眠60秒 1分钟
//                Thread.Sleep(60000);
//                RegisterMonitor();
//            });
//            task.Start();
//        }
//    }
//}
