using SqliteORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wpfClient.Model
{
    [Table]
    public class User : TableBase<User>
    {
        [PrimaryKey(true)]
        public long Id { get; set; }
        [Field]
        public string Code { get; set; }
        [Field]
        public string Pwd { get; set; }
        [Field]
        public DateTime CreateOn { get; set; }
        [Field]
        public bool IsReadPwd { get; set; }
    }
}
