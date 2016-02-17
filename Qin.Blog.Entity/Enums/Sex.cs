using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qin.Blog.Entity.Enums
{
    public enum Sex
    {
        /// <summary>
        /// 女
        /// </summary>
        [Description("女")]
        Female = 0,
 
        /// <summary>
        /// 男
        /// </summary>
        [Description("男")]
        Man = 1
    }
}
