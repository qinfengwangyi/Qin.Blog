using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qin.Blog.Entity.Enums
{
    /// <summary>
    /// 操作符
    /// </summary>
    public enum Operator
    {
        /// <summary>
        /// 等于
        /// </summary>
        [Description("=")]
        Equal = 1,

        /// <summary>
        /// 大于
        /// </summary>
        [Description(">")]
        Great = 2,

        /// <summary>
        /// 大于等于
        /// </summary>
        [Description(">=")]
        GreatEqual = 3,

        /// <summary>
        /// 小于
        /// </summary>
        [Description("<")]
        Less = 4,

        /// <summary>
        /// 小于等于
        /// </summary>
        [Description("<=")]
        LessEqual = 5,

        /// <summary>
        /// Like
        /// </summary>
        [Description("Like")]
        Like = 6,

        /// <summary>
        /// 升序
        /// </summary>
        [Description("Asc")]
        Asc = 7,

        /// <summary>
        /// 倒序
        /// </summary>
        [Description("Desc")]
        Desc = 8
    }
}
