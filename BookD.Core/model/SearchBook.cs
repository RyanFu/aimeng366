using All.Core;
using Db.EF.DbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookD.Core
{
    public class SearchBook : ResourceBase
    {
        /// <summary>
        /// 书名
        /// </summary>
        public string BookName { get; set; }
        /// <summary>
        /// 作者名
        /// </summary>
        public string AuthorName { get; set; }
        /// <summary>
        /// 书路径
        /// </summary>
        public string BookUrl { get; set; }
        /// <summary>
        /// 图书介绍
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 最后一章
        /// </summary>
        public string LastChapter { get; set; }
        /// <summary>
        /// 最后一章Url
        /// </summary>
        public string LastChapterUrl { get; set; }
        /// <summary>
        /// 图片路径
        /// </summary>
        public string ImgUrl { get; set; }

    }
}
