using All.Core;
using Db.DbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Db.EF.DbModel
{
    public class User : IBaseData, IDeleted, IChangeUser, IChangeTime
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// qq号
        /// </summary>
        public string QQ { get; set; }
        /// <summary>
        /// 手机号 
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 用户账号
        /// </summary>
        public string UserCode { get; set; }
        /// <summary>
        /// 用户密码
        /// </summary>
        public string UserPwd { get; set; }
        /// <summary>
        /// 用户类型
        /// </summary>
        public UserType UserType { get; set; }
        /// <summary>
        /// 用户是否通过验证
        /// </summary>
        public bool IsValidate { get; set; }

        public virtual ICollection<UserResourceIndex> UserResourceIndexs { get; set; }


        public bool Deleted { get; set; }

        public string ModifiedBy { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }
    }
}
