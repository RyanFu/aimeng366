using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace All.Core
{
    /// <summary>
    /// 返回值类型基类
    /// </summary>
    public class SResult
    {
        RState _state = RState.OK;

        string _value = "";

        /// <summary>
        /// 状态
        /// </summary>
        public RState RState
        {
            get
            {
                return _state;
            }
            set
            {
                _state = value;
            }
        }
        public string Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }
    }

    /// <summary>
    /// 返回值范型,只有一个返回值
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SResult<T> : SResult
    {
        /// <summary>
        /// 返回结果数据
        /// </summary>
        public T Resualt { get; set; }
    }
    public class SResult<T1, T2> : SResult
    {
        /// <summary>
        /// 返回数据1
        /// </summary>
        public T1 Resualt { get; set; }
        /// <summary>
        /// 返回数据2
        /// </summary>
        public T2 Resual2 { get; set; }
    }
    public class SResult<T1, T2, T3> : SResult
    {
        /// <summary>
        /// 返回数据1
        /// </summary>
        public T1 Resualt { get; set; }
        /// <summary>
        /// 返回数据2
        /// </summary>
        public T2 Resual2 { get; set; }
        public T3 Resual3 { get; set; }
    }
    /// <summary>
    /// 执行结果状态
    /// </summary>
    public enum RState
    {
        /// <summary>
        /// 正常，Sucess
        /// </summary>
        OK = 0,
        /// <summary>
        /// 发生错误，错误信息在Error属性中
        /// </summary>
        Error = 1,
        /// <summary>
        /// 密码错误，要求客户端跳转到登录页面
        /// </summary>
        PasswordError = 2,
        /// <summary>
        /// 版本太旧，要求客户端跳转到更新页面。
        /// </summary>
        VersionOld = 3,
        /// <summary>
        /// 特殊情况，要求锁定客户端。
        /// </summary>
        LockDevice = 4
    }
}
