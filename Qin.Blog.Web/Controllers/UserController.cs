using Qin.Blog.Entity;
using Qin.Blog.Extentions;
using Qin.Blog.ISerivce;
using Qin.Blog.Service;
using Qin.Blog.Web.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Qin.Blog.Web.Controllers
{
    public class UserController : BaseController
    {
        IUserService _IUserService = new UserService();
        // GET: User
        public ActionResult Index()
        {
            var a = _IUserService.Count("");
            var b = _IUserService.Exsit("qinfeng");

            return new ActionReturn(true);
        }



        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [LoginAuthorize]
        public ActionResult Update(User model)
        {
            model.Id = CUR_USER.Id;
            model.ModifyUser = CUR_USER.UserName;
            model.ModifyTime = CUR_USER.ModifyTime;
            if (_IUserService.Update(model) > 0)
            {
                CUR_USER.NickName = model.NickName;
                CUR_USER.Sex = model.Sex;
                return new ActionReturn(true, "修改成功！", null);
            }
            return new ActionReturn(false, "修改失败！", null);
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="id"></param>
        /// <param name="oldPwd"></param>
        /// <param name="newPwd"></param>
        /// <returns></returns>
        [HttpPost]
        [LoginAuthorize]
        public ActionResult EditPwd(string oldPwd, string newPwd)
        {
            string msg = string.Empty;
            if (_IUserService.EditPwd(CUR_USER.Id, oldPwd, newPwd, out msg))
            {
                return new ActionReturn(true, "修改成功！", null);
            }
            return new ActionReturn(false, msg, null);
        }
    }
}