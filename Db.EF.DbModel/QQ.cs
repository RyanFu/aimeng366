using All.Core;
using Db.EF;
using Db.EF.DbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Db
{
    /// <summary>
    /// qq好嘛
    /// </summary>
    public class QQ : IBaseData, IDeleted, IChangeUser, IChangeTime
    {
        public int Id { get; set; }
        public int pid { get; set; }
        //发送消息用的标示  不是qq
        public string Uin { get; set; }
        public string QQNum { get; set; }
        public string Name { get; set; }
        public QQType QQType { get; set; }


        public bool Deleted { get; set; }

        public string ModifiedBy { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }
    }
}
