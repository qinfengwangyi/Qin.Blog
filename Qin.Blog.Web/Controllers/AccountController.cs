using log4net;
using Qin.Blog.Entity;
using Qin.Blog.Extentions;
using Qin.Blog.ISerivce;
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
    public class AccountController : BaseController
    {
        IUserService _IUserService = new UserService();
        IArticleService _IArticleService = new ArticleService();

        /// <summary>
        /// 用户中心
        /// </summary>
        /// <returns></returns>
        [LoginAuthorize]
        public ActionResult Index()
        {
            int total = 0;
            ViewBag.UserInfo = CUR_USER;
            List<Article> list = _IArticleService.GetUserArticles(CUR_USER.Id, out total);
            return View(list);
        }



        public ActionResult Login()
        {
            return View();
        }


        public ActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(string userName, string passWord)
        {
            User user = new User();
            string msg = string.Empty;
            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(passWord))
            {
                var result = _IUserService.LogOn(userName, passWord, out user, out msg);
                if (result)
                {
                    Session["CUR_USER"] = user;
                    return new ActionReturn(true, msg, null);//登录成功
                }
                else
                {
                    return new ActionReturn(false, msg, null);
                }
            }
            else
            {
                return new ActionReturn(false, msg = "用户名或密码为空", "/Account/LogOn");
            }
        }

        /// <summary>
        /// 注销
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            Session["CUR_USER"] = null;
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Register(string userName, string passWord)
        {
            var user = new User()
            {
                Id = Guid.NewGuid().ToId(),
                UserName = userName,
                NickName = userName,
                PassWord = passWord,
                UserResource = "Web Register",
                UserType = 2,
                CreateTime = DateTime.Now,
                CreateUser = userName,
            };
            if (_IUserService.Insert(user))
            {
                return new ActionReturn(true, "注册成功", null);
            }
            return new ActionReturn(false);
        }


        /// <summary>
        /// 检查该名称是否存在
        /// </summary>
        /// <param name="registerUserName"></param>
        /// <returns>存在：true/不存在：false</returns>
        public ActionResult CheckUserName(string userName)
        {
            var result = _IUserService.Exsit(userName);
            if (result)
            {
                return new ActionReturn(result, "该用户名已存在，不能注册", null);
            }
            return new ActionReturn(result, "该用户名可以注册", null);
        }
    }
}