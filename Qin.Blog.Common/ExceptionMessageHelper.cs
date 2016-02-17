using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qin.Blog.Common
{
    /// <summary>
    ///     异常信息封装类
    /// </summary>
    public class ExceptionMessageHelper
    {
        #region 构造函数
        /// <summary>
        ///     以自定义用户信息和异常对象实例化一个异常信息对象
        /// </summary>
        /// <param name="e"> 异常对象 </param>
        /// <param name="userMessage"> 自定义用户信息 </param>
        /// <param name="isHideStackTrace"> 是否隐藏异常堆栈信息 </param>
        public ExceptionMessageHelper(Exception e, string userMessage = null, bool isHideStackTrace = false)
        {
            UserMessage = string.IsNullOrEmpty(userMessage) ? e.Message : userMessage;

            StringBuilder sb = new StringBuilder();
            ExMessage = string.Empty;
            int count = 0;
            string appString = "";
            while (e != null)
            {
                if (count > 0)
                {
                    appString += "　";
                }
                ExMessage = e.Message;
                ExMethod = e.TargetSite == null ? "" : string.Format("{0}.{1}", e.TargetSite.ReflectedType.Name, e.TargetSite.Name);
                sb.AppendLine(string.Format("{0}异常消息：{1}", appString, e.Message));
                sb.AppendLine(string.Format("{0}异常类型：{1}", appString, e.GetType().FullName));
                sb.AppendLine(appString + e.TargetSite == null ? "异常方法：Null" : string.Format("异常方法：{0}.{1}", e.TargetSite.ReflectedType.Name, e.TargetSite.Name));
                sb.AppendLine(string.Format("{0}异常源：{1}", appString, e.Source));
                if (!isHideStackTrace && e.StackTrace != null)
                {
                    sb.AppendLine(string.Format("{0}异常堆栈：{1}", appString, e.StackTrace));
                }
                if (e.InnerException != null)
                {
                    sb.AppendLine(string.Format("{0}内部异常：", appString));
                    count++;
                }
                e = e.InnerException;
            }
            ErrorDetails = sb.ToString();
            sb.Clear();
        }
        #endregion

        #region 属性

        /// <summary>
        ///     用户信息，用于报告给用户的异常消息
        /// </summary>
        public string UserMessage { get; set; }

        /// <summary>
        ///     直接的Exception异常信息，即e.Message属性值
        /// </summary>
        public string ExMessage { get; private set; }

        /// <summary>
        ///     异常输出的详细描述，包含异常消息，规模信息，异常类型，异常源，引发异常的方法及内部异常信息
        /// </summary>
        public string ErrorDetails { get; private set; }

        /// <summary>
        /// 发生异常的方法
        /// 格式：类名.方法名
        /// </summary>
        public string ExMethod { get; private set; }

        #endregion
    }
}
