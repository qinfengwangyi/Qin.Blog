using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qin.Blog.Entity.DBModel
{
    public class CommentDBModel : BaseEntity
    {
        /// <summary>
        /// 文章ID
        /// </summary>
        public string ArticleId { get; set; }

        /// <summary>
        /// 父级ID
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 评论内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 评论者ID
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 评论者昵称
        /// </summary>
        public string NickName { get; set; }


        #region 被回复的评论

        /// <summary>
        /// 父级评论的ID
        /// </summary>
        public string PId { get; set; }

        /// <summary>
        /// 父级评论内容
        /// </summary>
        public string PContent { get; set; }

        /// <summary>
        /// 父级评论者ID
        /// </summary>
        public string PUserId { get; set; }

        /// <summary>
        /// 父级评论者昵称
        /// </summary>
        public string PNickName { get; set; }

        /// <summary>
        /// 父级留言者
        /// </summary>
        public string PCreateUser { get; set; }

        /// <summary>
        /// 父级留言时间
        /// </summary>
        public DateTime PCreateTime { get; set; }

        #endregion
    }
}
