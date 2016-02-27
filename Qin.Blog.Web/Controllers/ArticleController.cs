using log4net;
using Qin.Blog.Entity;
using Qin.Blog.Entity.DBModel;
using Qin.Blog.Extentions;
using Qin.Blog.IService;
using Qin.Blog.Service;
using Qin.Blog.Web.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Qin.Blog.Web.Controllers
{
    public class ArticleController : BaseController
    {
        IArticleService _IArticleService = new ArticleService();
        ICommentService _ICommentService = new CommentService();


        /// <summary>
        /// 异步示例
        /// </summary>
        /// <returns></returns>
        public async System.Threading.Tasks.Task<ActionResult> AsyncTask()
        {
            int total = 0;
            await System.Threading.Tasks.Task.Run(() =>
            {
                var list = _IArticleService.Pages(1, 5, "", out total);
                return new ActionReturn(list);
            });
            return new ActionReturn(false);
        }

        
        public ActionResult Index()
        {
            int total = 0;
            var list = _IArticleService.Pages(1, 10, null, out total);
            ViewBag.ArticleList = list;
            ViewBag.Total = total;
            return View();
        }

        /// <summary>
        /// 编辑文章
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit()
        {
            return View();
        }

        
        /// <summary>
        /// 标签分类文章
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult TagPages(int pageIndex = 1, int pageSize = 10)
        {
            int total = 0;
            int curPageCount = 0;
            var list = _IArticleService.Pages(pageIndex, pageSize, null, out total);
            if (list != null && list.Count > 0)
            {
                return new PagesReturn(pageIndex, total, list, curPageCount = list.Count);
            }
            return new ActionReturn(false, "没有更多文章", "");
        }



        /// <summary>
        /// 加载分类文章
        /// </summary>
        /// <param name="index"></param>
        /// <param name="pageIndex"></param>
        /// <returns>return index category article, no user</returns>
        public ActionResult CategoryPage(string index, int pageIndex = 1)
        {
            pageIndex = pageIndex <= 1 ? 1 : pageIndex;
            if (!string.IsNullOrEmpty(index))
            {
                int total = 0;
                var list = _IArticleService.CategoryPage(pageIndex, 10, index, out total);  //默认每页10条
                if (list != null && list.Count > 0)
                {
                    return new PagesReturn(pageIndex, total, list, list.Count);
                }
            }
            return new ActionReturn(false, "没有更多文章", "");
        }

        /// <summary>
        /// 添加文章
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ValidateInput(false)]
        [LoginAuthorize]
        public ActionResult Insert(Article model)
        {
            model.Id = Guid.NewGuid().ToString("N");
            model.UserId = CUR_USER.Id;  //用户ID
            model.CreateTime = DateTime.Now;
            model.CreateUser = CUR_USER.UserName;
            model.ModifyTime = DateTime.Now;
            model.ModifyUser = CUR_USER.UserName;
            var list = _IArticleService.Insert(model);
            return AlertMsgAndJs("添加成功！", "history.go(-1);");
        }

        /// <summary>
        /// 文章详细内容
        /// </summary>
        /// <param name="id">文章ID</param>
        /// <returns></returns>
        public ActionResult Detail(string id)
        {
            int total = 0;
            ArticleDBModel model = _IArticleService.GetArticleById(id);
            var commentList = _ICommentService.CommentPages(1, 10, id, out total);
            return View(model);
        }

        /// <summary>
        /// 编辑文章(Id必须存在)
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        [HttpPost]
        [LoginAuthorize]
        public ActionResult Update(Article article)
        {
            article.ModifyTime = DateTime.Now;
            article.ModifyUser = CUR_USER.UserName;
            var result = _IArticleService.Update(article);
            return new ActionReturn(true);
        }
    }
}