using All.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Db.DbModel
{
    public class UpdateInfo : IBaseData, IDeleted, IChangeUser, IChangeTime
    {
        public int Id { get; set; }
        public string AppName { get; set; }
        /// <summary>
        /// 应用程序版本
        /// </summary>
        public string AppVersion { get; set; }
        /// <summary>
        /// 更新描述
        /// </summary>
        public string Desc { get; set; }



        public bool Deleted { get; set; }

        public string ModifiedBy { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }

        
    }
}
