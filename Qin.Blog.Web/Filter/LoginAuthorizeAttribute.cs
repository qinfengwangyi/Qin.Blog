using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Qin.Blog.Web.Filter
{
    public class LoginAuthorizeAttribute : AuthorizeAttribute
    {
        #region 构造函数

        public LoginAuthorizeAttribute()
        {
        }

        #endregion

        #region AuthorizeAttribute 成员

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var user = filterContext.HttpContext.Session["CUR_USER"];
            if (user == null)
            {
                UrlHelper Url = new UrlHelper(filterContext.RequestContext);
                string url = Url.Action("Login", "Account", new { area = "" });
                filterContext.HttpContext.Response.Redirect(url, true);
            }
        }

        #endregion
    }

    public class NoLogOnAuthorizeAttribute : AuthorizeAttribute
    {
        public NoLogOnAuthorizeAttribute()
        {
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {

        }
    }

    /// <summary>
    /// 后台登录授权
    /// </summary>
    public class AdminLoginAuthorizeAttribute : AuthorizeAttribute
    {
        #region 构造函数

        public AdminLoginAuthorizeAttribute()
        {
        }

        #endregion

        #region AdminAuthorizeAttribute 成员

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var user = filterContext.HttpContext.Session["ADMIN_USER"];
            if (user == null)
            {
                filterContext.HttpContext.Response.Redirect("/UserCenter/Home/Login");
            }
        }

        #endregion
    }
}