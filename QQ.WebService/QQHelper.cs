using All.Core;
using All.Helper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using Db.EF.DbModel;
using Db;
using All.Manager;

namespace QQ.WebService
{
    public class QQHelper : ManagerBase<Db.QQ>
    {
        public HttpHelper httpHelper = new HttpHelper();
        public string clientid { get; set; }
        public string ptwebqq { get; set; }
        public string psessionid { get; set; }
        public string Number { get; set; }

        public Db.QQ NowQQ { get; set; }

        public QQHelper(string number)
        {
            Number = number;
        }

        /// <summary>
        /// 检查QQ如果没有的话就添加
        /// </summary>
        public void CheckQQ()
        {
            var data = Repo.GetAll().FirstOrDefault(r => r.QQNum == Number);
            if (data == null)
            {
                data = new Db.QQ()
                {
                    pid = 0,
                    QQNum = Number,
                    QQType = QQType.system,
                    Uin = "",
                    Name = Number
                };
                Repo.Insert(data);
            }
            Repo.Save();
            NowQQ = data;
        }

        //发送消息
        public void SendMsg(string _msg, string toQQ)
        {
            var url = "http://d.web2.qq.com/channel/send_buddy_msg2";
            var refer = "http://d.web2.qq.com/proxy.html?v=20110331002&callback=1&id=3";
            var unMsg = "{\"to\":" + toQQ + ",\"face\":0,\"content\":\"[\\\"" + _msg + "\\\",[\\\"font\\\",{\\\"name\\\":\\\"宋体\\\",\\\"size\\\":\\\"10\\\",\\\"style\\\":[0,0,0],\\\"color\\\":\\\"000000\\\"}]]\",\"msg_id\":37980006,\"clientid\":\"" + clientid + "\",\"psessionid\":\"" + psessionid + "\"}";
            unMsg = UrlEncode(unMsg);
            var msg = "r=" + unMsg + "&clientid=" + clientid + "&psessionid=" + psessionid;
            try
            {
                var rst = httpHelper.GetHtml(url, refer, Encoding.UTF8.GetBytes(msg));
                //处理发送结果

                var result = "";
            }
            catch (Exception e)
            {
                LogHelper.error(e.Message);
            }

        }
        //获取消息
        public void GetMsg()
        {
            string url = "http://d.web2.qq.com/channel/poll2";
            string refer = "http://d.web2.qq.com/proxy.html?v=20110331002&callback=1&id=3";
            var xu = "{\"clientid\":\"" + clientid + "\",\"psessionid\":\"" + psessionid + "\",\"key\":0,\"ids\":[]}";
            xu = UrlEncode(xu);
            var msg = "r=" + xu + "&clientid=" + clientid + "&psessionid=" + psessionid;
            //接收消息
            try
            {
                var rst = httpHelper.GetResponse(url, refer, Encoding.UTF8.GetBytes(msg), int.MaxValue);
                if (rst.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    Thread.Sleep(1);
                    GetMsg();
                    return;
                }
                //处理消息

                DoMsg(httpHelper.GetHtml(rst));

            }
            catch (Exception ex)
            {
                LogHelper.error(ex.Message);
                //处理完异常继续等待消息
                GetMsg();
            }
            //处理完消息  递归调用继续接收消息
            GetMsg();
        }

        /// <summary>
        /// 处理消息
        /// </summary>
        private void DoMsg(string msg)
        {
            var data = GetJsonDictionary(msg);
            if (data["retcode"] != null && data["retcode"].ToString() == "0")
            {
                switch (data["poll_type"].ToString())
                {
                    case "system_message":
                        if (data["type"] != null && data["type"] == "added_buddy_sig")
                        {
                            Repo.Insert(new Db.QQ()
                            {
                                Name = data["account"].ToString(),
                                pid = NowQQ.Id,
                                QQNum = data["account"].ToString(),
                                QQType = QQType.friend,
                                Uin = data["from_uin"].ToString()
                            });
                        }
                        break;
                }
            }
        }

        public void AddCookie(string cookies)
        {
            var cks = cookies.Split(';');
            var rst = new Dictionary<string, string>();
            foreach (var item in cks)
            {
                var nv = item.Split('=');
                var p1 = nv[0];
                var p2 = new string(item.Skip(item.IndexOf('=') + 1).ToArray());
                if (rst.ContainsKey(p1.Trim()))
                {
                    if (p2.Trim() != "")
                    {
                        rst[p1.Trim()] = p2.Trim();
                    }

                }
                else
                {
                    rst.Add(p1.Trim(), p2.Trim());
                }
            }
            httpHelper.AddCookies(rst);
        }






        /// <summary>
        /// 将json数据反序列化为Dictionary
        /// </summary>
        /// <param name="jsonData">json数据</param>
        /// <returns></returns>
        public Dictionary<string, object> GetJsonDictionary(string jsonData)
        {
            //实例化JavaScriptSerializer类的新实例
            JavaScriptSerializer jss = new JavaScriptSerializer();
            try
            {
                //将指定的 JSON 字符串转换为 Dictionary<string, object> 类型的对象
                return jss.Deserialize<Dictionary<string, object>>(jsonData);
            }
            catch (Exception ex)
            {
                LogHelper.error(ex.Message);
                return null;
            }
        }
        public string UrlEncode(string code)
        {
            return System.Web.HttpUtility.UrlEncode(code, Encoding.UTF8);
        }

    }


}
