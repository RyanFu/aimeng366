using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace All.Core
{

    //IBaseData,IDeleted,IChangeUser,IChangeTime ,IWBS,IVersion

    /// <summary>
    /// 实体主键接口
    /// </summary>
    public interface IBaseData
    {
        int Id { get; set; }
    }
    /// <summary>
    /// 支持并发控制的接口
    /// </summary>
    public interface IVersion
    {
        byte[] Version { get; set; }
    }
    /// <summary>
    /// 支持逻辑删除接口
    /// </summary>
    public interface IDeleted
    {
        /// <summary>
        /// 是否已删除
        /// </summary>
        bool Deleted { get; set; }
    }
    /// <summary>
    /// 支持WBS编码接口
    /// </summary>
    public interface IWBS
    {
        /// <summary>
        /// 编码，服务端自动处理，在客户端需要用到
        /// </summary>
        string Code { get; set; }
        /// <summary>
        /// 父编码
        /// </summary>
        string ParentCode { get; set; }
        /// <summary>
        /// 深度
        /// </summary>
        int Deep { get; set; }
    }
    /// <summary>
    /// 数据更改用户记录（原Subsonic中的）
    /// </summary>
    public interface IChangeUser
    {
        /// <summary>
        /// 修改人
        /// </summary>
        string ModifiedBy { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        string CreatedBy { get; set; }
    }
    /// <summary>
    /// 数据更改时间记录(原Subsonic中的)
    /// </summary>
    public interface IChangeTime
    {
        DateTime CreatedOn { get; set; }
        DateTime ModifiedOn { get; set; }
    }
}
