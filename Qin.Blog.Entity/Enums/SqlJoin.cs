using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qin.Blog.Entity.Enums
{
    /// <summary>
    /// 连接符
    /// </summary>
    public enum SqlJoin
    {
        /// <summary>
        /// And连接
        /// </summary>
        [Description("And")]
        And =1,

        /// <summary>
        /// Or连接
        /// </summary>
        [Description("Or")]
        Or = 2,

        /// <summary>
        /// 排序
        /// </summary>
        [Description("Order By")]
        OrderBy = 3,

        /// <summary>
        /// 分组
        /// </summary>
        [Description("Group By")]
        GroupBy = 4,
    }
}
