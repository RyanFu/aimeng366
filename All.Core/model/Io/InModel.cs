using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web;

namespace All.Core
{

    public class Input
    {
        /// <summary>
        /// 验证对象
        /// </summary>
        /// <returns>错误消息，验证通过返回null</returns>
        public string ValidateMe()
        {
            ValidationContext context = new ValidationContext(this, null, null);
            List<ValidationResult> rst = new List<ValidationResult>();
            try
            {
                if (!Validator.TryValidateObject(this, context, rst, true))
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (var item in rst)
                    {
                        sb.AppendLine(item.ErrorMessage);
                    }
                    return sb.ToString();
                }
                return null;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

    }
    /// <summary>
    /// 输入模型，泛型应用于只有一个输入值的情况
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Input<T> : Input
    {
        /// <summary>
        /// 输入参数
        /// </summary>
        public T InputPara { get; set; }
    }
    /// <summary>
    /// 输入模型，泛型应用于有两个输入值的情况
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    public class Input<T1, T2> : Input
    {
        public T1 InputPara { get; set; }
        public T2 InputPara2 { get; set; }
    }
    public class Input<T1, T2, T3> : Input
    {
        public T1 InputPara { get; set; }
        public T2 InputPara2 { get; set; }
        public T3 InputPara3 { get; set; }
    }
    public class Input<T1, T2, T3, T4> : Input
    {
        public T1 InputPara { get; set; }
        public T2 InputPara2 { get; set; }
        public T3 InputPara3 { get; set; }
        public T4 InputPara4 { get; set; }
    }
}