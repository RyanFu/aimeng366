using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookD.Web.Models
{
    public class UserMap
    {
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
        /// 用户名  默认为QQ昵称（如果通过QQ自动报备的话）
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 用户账号 （默认为QQ号）
        /// </summary>
        [Required(ErrorMessage="请填写账号")]
        public string UserCode { get; set; }
        /// <summary>
        /// 用户密码  （默认为QQ后6位  不够6位补0）
        /// </summary>
        [Required(ErrorMessage = "请填写密码")]
        public string UserPwd { get; set; }
    }
}