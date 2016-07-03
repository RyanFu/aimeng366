using All.Core;
using Db.DbModel;
using Db.EF;
using Db.EF.DbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Db.EF.DbModel
{
    /// <summary>
    /// 书籍信息
    /// </summary>
    public class Book : IBaseData, IDeleted, IChangeUser, IChangeTime
    {
        public int ResourceIndexId { get; set; }
        public string AuthorName { get; set; }
        /// <summary>
        /// 简介
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 图片路径
        /// </summary>
        public string ImgUrl { get; set; }

        /// <summary>
        /// 最后一章
        /// </summary>
        public string LastChapter { get; set; }
        /// <summary>
        /// 最后一章Url
        /// </summary>
        public string LastChapterUrl { get; set; }

        public virtual ResourceIndex ResourceIndex { get; set; }
        public bool Deleted { get; set; }
        public string ModifiedBy { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }


        int IBaseData.Id
        {
            get
            {
                return ResourceIndexId;
            }
            set
            {
                ResourceIndexId = value;
            }
        }


        
    }
}
