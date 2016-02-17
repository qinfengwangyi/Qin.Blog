using Qin.Blog.Entity;
using Qin.Blog.ISerivce;
using Qin.Blog.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Qin.Blog.Web.Controllers
{
    public class BaseController : Controller
    {
        //系统日志
        protected ISysLogService _ISysLogService = new SysLogService();


        private User _CUR_USER = null;
        public User CUR_USER
        {
            get
            {
                if (_CUR_USER == null)
                {
                    _CUR_USER = HttpContext.Session["CUR_USER"] as User;
                }
                return _CUR_USER;
            }
        }

        public class SupportInfo
        {
            public string Navindex { get; set; }
        }

        private SupportInfo _Navactive = null;
        public SupportInfo Navactive
        {
            get
            {
                if (_Navactive == null)
                {
                    _Navactive = new SupportInfo() { Navindex = HttpContext.Session["Navindex"] as string };
                }
                return _Navactive;
            }
            set { _Navactive = value; }
        }



        #region 表单提交时的提示

        protected ActionResult AlertMsg(string msg)
        {
            string script = string.Format("<script>alert('{0}');</script>", msg);
            Response.Write(script);
            Response.End();
            return new EmptyResult();
        }

        protected ActionResult AlertMsg(string msg, string returnUrl)
        {
            var script = string.Format("<script>alert('{0}');this.location.href='{1}';</script>", msg, returnUrl);
            Response.Write(script);
            Response.End();
            return new EmptyResult();
        }

        protected ActionResult AlertMsgAndJs(string msg, string js)
        {
            string script = string.Format("<script>alert('{0}');{1};</script>", msg, js);
            Response.Write(script);
            Response.End();
            return new EmptyResult();
        }

        #endregion
    }
}