using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qin.Blog.Entity.DBModel
{
    public class LeaveMsgDBModel: BaseEntity
    {
        /// <summary>
        /// 父级ID
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 留言内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 留言者ID
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 留言者昵称
        /// </summary>
        public string NickName { get; set; }


        #region 被回复留言

        /// <summary>
        /// 父级留言的ID
        /// </summary>
        public string PId { get; set; }

        /// <summary>
        /// 父级留言内容
        /// </summary>
        public string PContent { get; set; }

        /// <summary>
        /// 父级留言者ID
        /// </summary>
        public string PUserId { get; set; }

        /// <summary>
        /// 父级留言者昵称
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
