using All.Core;
using Db.EF;
using Db.EF.DbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Db.DbModel
{
    /// <summary>
    /// 资源信息 包括小说资源  视频资源的 索引
    /// </summary>
    public class ResourceIndex : IBaseData, IDeleted, IChangeUser, IChangeTime
    {
        public int Id { get; set; }
        public string ResourceId { get; set; }
        /// <summary>
        /// 资源名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 资源类型
        /// </summary>
        public ResourceType ResourceType { get; set; }
        /// <summary>
        /// 资源来源网站
        /// </summary>
        public ResourceFromSite ResourceFromsite { get; set; }
        /// <summary>
        /// 资源Url
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 订阅数量  （如果订阅数量为0 就不检索）
        /// </summary>
        public int SubscribeNum { get; set; }
        public virtual ICollection<UserResourceIndex> UserResourceIndexs { get; set; }


        public bool Deleted { get; set; }

        public string ModifiedBy { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }
    }
}
