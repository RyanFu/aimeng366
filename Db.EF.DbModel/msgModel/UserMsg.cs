using All.Core;
using Db.EF.DbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Db.DbModel
{
    public class UserMsg:IBaseData, IDeleted, IChangeUser, IChangeTime
    {
        public int Id { get; set; }
        public virtual User User { get; set; }
        public int ToUserId { get; set; }
        public string Msg { get; set; }
        public bool IsRead { get; set; }


        public bool Deleted { get; set; }
        public string ModifiedBy { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
