using Qin.Blog.Extentions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Qin.Blog.Web.Areas.UserCenter.Controllers
{
    public class ArticleController : Controller
    {


        /// <summary>
        /// 文章列表
        /// </summary>
        /// <returns></returns>
        public ActionResult Pages()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Pages(int pageIndex = 1, int pageSize = 10)
        {
            return new ActionReturn("");
        }
    }
}