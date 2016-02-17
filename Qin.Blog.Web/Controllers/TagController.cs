using Qin.Blog.Entity;
using Qin.Blog.Extentions;
using Qin.Blog.ISerivce;
using Qin.Blog.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Qin.Blog.Web.Controllers
{
    public class TagController : BaseController
    {
        ITagService _ITagService = new TagService();

        // GET: Tag
        public ActionResult GetList()
        {
            int total = 0;
            var list = _ITagService.GetList(out total);
            if (list != null && list.Count > 0)
            {
                return new ActionReturn(list);
            }
            return new ActionReturn(false);
        }

        /// <summary>
        /// 添加标签
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public ActionResult Insert(Tag tag)
        {
            var model = new Tag
            {
                Id = Guid.NewGuid().ToString("N"),
                TagName = tag.TagName,
                CreateTime = DateTime.Now,
                CreateUser = CUR_USER.UserName,
                ModifyTime = DateTime.Now,
                ModifyUser = CUR_USER.UserName
            };
            if (_ITagService.Insert(model))
            {
                return new ActionReturn(true);
            }
            return new ActionReturn(false);
        }

    }
}