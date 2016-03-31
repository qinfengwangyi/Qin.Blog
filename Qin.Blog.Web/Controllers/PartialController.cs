using Qin.Blog.IService;
using Qin.Blog.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Qin.Blog.Web.Controllers
{
    public class PartialController : BaseController
    {
        IArticleTypeService _IArticleTypeService = new ArticleTypeService();
        ITagService _ITagService = new TagService();

        /// <summary>
        /// 标签视图
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult TagHtml()
        {
            int total = 0;
            var list = _ITagService.GetList(out total);
            ViewBag.TagIndex = CUR_SupportInfo == null ? null : CUR_SupportInfo.TagIndex;
            return PartialView(list);
        }


        [ChildActionOnly]
        public ActionResult NavHtml()
        {
            ViewBag.User = CUR_USER;
            ViewBag.Navactive = CUR_SupportInfo == null ? 0 : CUR_SupportInfo.Navindex;
            return PartialView();
        }

    }
}