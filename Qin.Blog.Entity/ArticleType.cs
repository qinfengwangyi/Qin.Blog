using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qin.Blog.Entity
{
    public class ArticleType : BaseEntity
    {
        /// <summary>
        /// 类型名称
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// 类型描述
        /// </summary>
        public string Mark { get; set; }

    }
}
