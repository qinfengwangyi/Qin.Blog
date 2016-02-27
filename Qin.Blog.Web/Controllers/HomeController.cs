using log4net;
using Qin.Blog.Entity.Enums;
using Qin.Blog.Entity;
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
using System.ComponentModel;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace Qin.Blog.Web.Controllers
{
    public class HomeController : BaseController
    {
        IArticleService _IArticleService = new ArticleService();
        ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public async Task<ActionResult> test()
        {
            Stopwatch timer1 = new Stopwatch();
            timer1.Start();
            Thread.Sleep(1000);
            timer1.Stop();
            var a = _IArticleService.Count("");

            return await Task.Run(() =>
            {
                Stopwatch timer2 = new Stopwatch();
                timer2.Start();
                Thread.Sleep(4000);
                timer2.Stop();
                return new ActionReturn("timer1:" + timer1.Elapsed + "；timer2:" + timer2.Elapsed);
            });
        }

        /// <summary>
        /// 网站首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(int index = 0)
        {
            Session["Navindex"] = index;//导航

            int total = 0;
            var list = _IArticleService.CategoryPage(1, 10, index.ToString(), out total);
            ViewBag.Total = total;
            ViewBag.Loaded = (total <= 10 ? total : 10);
            ViewBag.Index = index;
            return View(list);
        }

        /// <summary>
        /// 添加文章页面
        /// </summary>
        /// <returns></returns>
        [LoginAuthorize]
        public ActionResult AddArticle()
        {
            return View();
        }




        /// <summary>
        /// Html板块
        /// </summary>
        /// <returns></returns>
        public ActionResult Html(int pageIndex = 1)
        {
            return new ActionReturn(223);
        }

        public ActionResult ASP_NET(int pageIndex = 1)
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        public ActionResult Essay(int pageIndex = 1)
        {
            return View();
        }


        public ActionResult LeaveMessage(int pageIndex = 1)
        {
            return View();
        }

    }


}