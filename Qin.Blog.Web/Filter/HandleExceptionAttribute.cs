using Qin.Blog.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Qin.Blog.Extentions;
using log4net;
using System.Reflection;

namespace Qin.Blog.Web.Filter
{
    public class HandleExceptionAttribute : HandleErrorAttribute
    {

        ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// 异常处理
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnException(ExceptionContext filterContext)
        {
            var url = RequestHelper.GetUrl();
            var referrer = RequestHelper.GetUrlReferrer();
            var ip = RequestHelper.GetIP();
            var parameters = RequestHelper.IsPost()
                ? filterContext.HttpContext.Request.Form.ToUrlString()
                : filterContext.HttpContext.Request.QueryString.ToUrlString();
            var exceptionMsg = new ExceptionMessageHelper(filterContext.Exception);
            StringBuilder expMsg = new StringBuilder();
            expMsg.AppendFormat("Ip：{0} \r\nUrl：{1} \r\nReferrer：{2} \r\nParameters：{3} \r\nRequestType：{4} \r\n", ip, url, referrer,
                parameters, filterContext.HttpContext.Request.RequestType);
            expMsg.Append(exceptionMsg.ErrorDetails);
            //Logger.GetLogger().Error(expMsg.ToString());
            log.Error(expMsg.ToString());
            if (filterContext.HttpContext.Request.IsAjaxRequest())
                filterContext.Result = new JsonResult
                {
                    Data = new ActionReturn(false, exceptionMsg.ExMessage),
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            base.OnException(filterContext);
        }
    }
}