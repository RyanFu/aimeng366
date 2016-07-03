using All.Core;
using Db.EF.DbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Db.DbModel
{
    /// <summary>
    /// 对用户的书本更新信息
    /// </summary>
    public class BookUserMsg : IBaseData
    {
        public int UserMsgId { get; set; }
        public virtual UserMsg UserMsg { get; set; }
        public int BookId { get; set; }
        public virtual Book Book { get; set; }
        /// <summary>
        /// 最后一章
        /// </summary>
        public string LastChapter { get; set; }
        /// <summary>
        /// 最后一章Url
        /// </summary>
        public string LastChapterUrl { get; set; }
        int IBaseData.Id
        {
            get
            {
                return UserMsgId;
            }
            set
            {
                UserMsgId = value;
            }
        }
    }
}
