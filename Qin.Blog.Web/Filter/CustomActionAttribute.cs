using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Qin.Blog.Web.Filter
{
    public class CustomActionAttribute : IActionFilter
    {
        //private Stopwatch timer;

        
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            throw new NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            throw new NotImplementedException();
        }
    }

    public class LogOnActionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {

        }
        //
        // 摘要: 
        //     在执行操作方法之前由 ASP.NET MVC 框架调用。
        //
        // 参数: 
        //   filterContext:
        //     筛选器上下文。
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

        }
        //
        // 摘要: 
        //     在执行操作结果后由 ASP.NET MVC 框架调用。
        //
        // 参数: 
        //   filterContext:
        //     筛选器上下文。
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {

        }
        //
        // 摘要: 
        //     在执行操作结果之前由 ASP.NET MVC 框架调用。
        //
        // 参数: 
        //   filterContext:
        //     筛选器上下文。
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {

        }

    }
}