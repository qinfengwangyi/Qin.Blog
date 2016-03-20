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
    public class LeaveMessageController : BaseController
    {
        ILeaveMessageService _ILeaveMessageService = new LeaveMessageService();


        // GET: LeaveMessage
        public ActionResult Index()
        {
            CUR_SupportInfo.Navindex = 4;
            var total = 0;
            var list = _ILeaveMessageService.LeaveMsgPages(1, 10, "", out total);
            ViewBag.Total = total;
            ViewBag.Loaded = (total <= 10 ? total : 10);
            return View(list);
        }

        /// <summary>
        /// 留言
        /// </summary>
        /// <param name="content"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        [HttpPost]
        [StringFilter]
        [ValidateInput(false)]
        public ActionResult Insert(string content)
        {
            LeaveMessage model = new LeaveMessage()
            {
                Id = Guid.NewGuid().ToString("N"),
                ParentId = "0",
                Content = content,
                CreateTime = DateTime.Now,
                ModifyTime = DateTime.Now
            };
            if (CUR_USER != null)
            {
                model.UserId = CUR_USER.Id;
                model.ModifyUser = CUR_USER.UserName;
                model.CreateUser = CUR_USER.UserName;
            }
            if (_ILeaveMessageService.Insert(model))
            {
                return new ActionReturn(true, "留言成功！", null);
            }
            return new ActionReturn(false, "Oh，留言失败！请联系管理员...", null);
        }

        /// <summary>
        /// 回复留言
        /// </summary>
        /// <param name="content"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        [HttpPost]
        [StringFilter]
        [ValidateInput(false)]
        public ActionResult Reply(string content, string parentId = "0")
        {
            if (CUR_USER != null)
            {
                LeaveMessage model = new LeaveMessage()
                {
                    Id = Guid.NewGuid().ToId(),
                    ParentId = parentId,
                    Content = content,
                    UserId = CUR_USER.Id,
                    CreateUser = CUR_USER.UserName,
                    CreateTime = DateTime.Now,
                    ModifyUser = CUR_USER.UserName,
                    ModifyTime = DateTime.Now
                };
                if (_ILeaveMessageService.Insert(model))
                {
                    return new ActionReturn(true, "回复成功！", null);
                }
                return new ActionReturn(false, "回复失败！", null);
            }
            else
            {
                return new ActionReturn(false, "请先登录！", null);
            }
        }

        /// <summary>
        /// 留言分页
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public ActionResult Pages(int pageIndex = 1)
        {
            int total = 0;
            pageIndex = pageIndex <= 1 ? 1 : pageIndex;
            var list = _ILeaveMessageService.LeaveMsgPages(pageIndex, 10, "", out total);  //默认每页10条
            if (list != null && list.Count > 0)
            {
                return new PagesReturn(pageIndex, total, list, list.Count);
            }
            return new ActionReturn(false, "没有更多文章", "");
        }
    }
}