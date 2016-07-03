using SqliteORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using wpfClient.Model;

namespace wpfClient.Manager
{
    public class BookMessageManager
    {

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        public List<BookMessage> GetPage(int pageIndex, int pagesize, Where clause = null)
        {
            using (TableAdapter<BookMessage> adapter = TableAdapter<BookMessage>.Open())
            {
                if (clause == null)
                {
                    var data = adapter.Select().OrderByDescending(r => r.CreateOn).Skip(pagesize * pageIndex).Take(pagesize);
                    return data.ToList();
                }
                else
                {
                    var data = adapter.Select().Where(clause).OrderByDescending(r => r.CreateOn).Skip(pagesize * pageIndex).Take(pagesize);
                    return data.ToList();
                }

            }
        }

        public int Count()
        {
            using (TableAdapter<BookMessage> adapter = TableAdapter<BookMessage>.Open())
            {
                return adapter.Select().Count();
            }
        }

        /// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="msgId"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public long UpdateReadState(int msgId, bool state)
        {
            using (TableAdapter<BookMessage> adapter = TableAdapter<BookMessage>.Open())
            {
                var msg = adapter.Select().FirstOrDefault(r => r.Id == msgId);
                msg.IsRead = state;
                return adapter.CreateUpdate(msg);
            }
        }

        public IEnumerable<dynamic> GetAllBookNames()
        {
            using (DirectSql adapter = Activator.CreateInstance<DirectSql>())
            {
                var paras = new List<string>();
                paras.Add("id>-1");
                var data = adapter.ExecuteDynamic("select DISTINCT Name from BookMessage", paras).ToList<dynamic>();
                return data;
            }
        }
    }
}
