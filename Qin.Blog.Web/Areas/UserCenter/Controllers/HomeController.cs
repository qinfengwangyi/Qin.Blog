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

namespace Qin.Blog.Web.Areas.UserCenter.Controllers
{
    public class HomeController : BaseController
    {
        IUserService _IUserService = new UserService();


        //[AdminLoginAuthorize]
        public ActionResult Index()
        {

            return View();
        }


        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string userName, string passWord)
        {
            User user = new User();
            string msg = string.Empty;
            var result = _IUserService.LogOn(userName, passWord, out user, out msg);
            Session["ADMIN_USER"] = user;
            return new ActionReturn(result, msg, "");
        }
    }
}