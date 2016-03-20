using Qin.Blog.Entity;
using Qin.Blog.Extentions;
using Qin.Blog.IService;
using Qin.Blog.Service;
using Qin.Blog.Web.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Qin.Blog.Web.Controllers
{
    public class CommentController : BaseController
    {
        ICommentService _ICommentService = new CommentService();

        // GET: Comment
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult CommentList(string articleId)
        {
            int total = 0;
            var list = _ICommentService.CommentPages(1, int.MaxValue, articleId, out total);
            ViewBag.TotalComment = total;
            ViewBag.ArticleId = articleId;
            ViewBag.UserName = CUR_USER != null ? CUR_USER.UserName : null;
            return PartialView(list);
        }

        /// <summary>
        /// 添加评论
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        [HttpPost]
        [StringFilter]
        [ValidateInput(false)]
        public ActionResult Insert(string content, string articleId)
        {
            Comment model = new Comment()
            {
                Id = Guid.NewGuid().ToString("N"),
                ArticleId = articleId,
                ParentId = "0",
                Content = content,
                CreateTime = DateTime.Now,
                ModifyTime = DateTime.Now
            };
            if (CUR_USER != null)
            {
                model.UserId = CUR_USER.Id;//评论者ID
                model.CreateUser = CUR_USER.UserName;
                model.ModifyUser = CUR_USER.UserName;
            }
            if (_ICommentService.Insert(model))
            {
                return new ActionReturn(true, "评论成功！", null);
            }
            return new ActionReturn(false, "评论失败！", null);
        }


        /// <summary>
        /// 回复评论
        /// </summary>
        /// <param name="content"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        [HttpPost]
        [StringFilter]
        [ValidateInput(false)]
        public ActionResult Reply(string content, string articleId, string parentId = "0")
        {
            if (CUR_USER != null)
            {
                Comment model = new Comment()
                {
                    Id = Guid.NewGuid().ToString("N"),
                    ArticleId = articleId,
                    UserId = CUR_USER.Id,//回复者ID
                    ParentId = parentId,
                    Content = content,
                    CreateUser = CUR_USER.UserName,
                    CreateTime = DateTime.Now,
                    ModifyUser = CUR_USER.UserName,
                    ModifyTime = DateTime.Now
                };
                if (_ICommentService.Insert(model))
                {
                    return new ActionReturn(true, "回复成功！", null);
                }
                return new ActionReturn(false, "回复失败！", null);
            }
            return new ActionReturn(false, "请先登录！", null);
        }
    }
}