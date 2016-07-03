using log4net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace All.Helper
{
    public static class LogHelper
    {
        private static readonly ILog logInfo = LogManager.GetLogger("InfoAppender");
        private static readonly ILog logErr = LogManager.GetLogger("ErrorAppender");
        /// <summary>
        /// 记录正常的消息
        /// </summary>
        /// <param name="msg">消息内容</param>
        public static void info(string msg)
        {
            logInfo.Info(msg);
        }
        /// <summary>
        /// 记录异常信息
        /// </summary>
        /// <param name="msg">异常信息内容</param>
        public static void error(string msg,Exception e = null)
        {
            StackTrace stackTrace = new StackTrace();
            StackFrame stackFrame = stackTrace.GetFrame(1);
            MethodBase methodBase = stackFrame.GetMethod();
            if (stackFrame != null && methodBase != null)
            {
                logErr.Error("类名:" + methodBase.ReflectedType.Name + " 方法名:" + methodBase.Name + " 信息:" + msg,e);
            }
            else
            {
                logErr.Error(" 信息:" + msg);
            }
        }
    }
}
