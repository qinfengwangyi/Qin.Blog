using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qin.Blog.Entity
{
    public class SysLog : BaseEntity
    {
        public SysLog()
        {
            CreateTime = DateTime.Now;
        }

        /// <summary>
        /// 位置
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 错误
        /// </summary>
        public string Error { get; set; }

        /// <summary>
        /// RemoteHost
        /// </summary>
        public string RemoteHost { get; set; }

        /// <summary>
        /// 请求数据
        /// </summary>
        public string RequestData { get; set; }

        /// <summary>
        /// 浏览器
        /// </summary>
        public string Browser { get; set; }

        /// <summary>
        /// 请求相对地址
        /// </summary>
        public string ActionCode { get; set; }

    }
}
