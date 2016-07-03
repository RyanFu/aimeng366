using All.Core;
using Db.EF;
using Db.EF.DbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Db.DbModel
{
    public class UserResourceIndex : IBaseData
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ResourceIndexId { get; set; }
        public virtual User User { get; set; }
        public virtual ResourceIndex ResourceIndex { get; set; }
    }
}
