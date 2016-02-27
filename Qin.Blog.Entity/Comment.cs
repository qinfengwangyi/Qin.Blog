using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qin.Blog.Entity
{
    public class Comment : BaseEntity
    {
        /// <summary>
        /// 文章ID
        /// </summary>
        public string ArticleId { get; set; }

        /// <summary>
        /// 评论内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 父级ID
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        //public string NickName { get; set; }

        /// <summary>
        /// 是否是作者
        /// </summary>
        public bool IsAuthor { get; set; }

        /// <summary>
        /// 评论者ID
        /// </summary>
        public string UserId { get; set; }
    }
}
