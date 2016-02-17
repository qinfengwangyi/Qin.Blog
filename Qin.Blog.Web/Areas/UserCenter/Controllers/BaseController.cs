using Qin.Blog.Entity;
using Qin.Blog.Web.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Qin.Blog.Web.Areas.UserCenter.Controllers
{
    
    public class BaseController : Controller
    {
        private User _CUR_USER = null;
        public User CUR_USER
        {
            get
            {
                if (_CUR_USER == null)
                {
                    _CUR_USER = HttpContext.Session["ADMIN_USER"] as User;
                }
                return _CUR_USER;
            }
        }
    }
}