using Qin.Blog.Extentions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qin.Blog.Entity
{

    public class Article : BaseEntity
    {

        /// <summary>
        /// 类型ID
        /// </summary>
        public string TypeId { get; set; }

        /// <summary>
        /// 文章标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// 是否置顶
        /// </summary>
        public int IsTop { get; set; }

        /// <summary>
        /// 是否原创
        /// </summary>
        public int IsOriginal { get; set; }

        /// <summary>
        /// 文章摘要
        /// </summary>
        public string Abstract { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        public string Pics { get; set; }

        /// <summary>
        /// 文章内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 浏览量
        /// </summary>
        public int ViewCount { get; set; }

        /// <summary>
        /// 给力
        /// </summary>
        public int Praise { get; set; }

        /// <summary>
        /// 不给力
        /// </summary>
        public int Contempt { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserId { get; set; }

    }
}
