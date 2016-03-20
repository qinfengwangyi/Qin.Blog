using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Qin.Blog.Web.Filter
{
    public class StringFilterAttribute : ActionFilterAttribute
    {
        private static string str = ConfigurationManager.AppSettings["SensitiveChar"].ToString();
        private static string[] arr = str.Split('|');

        public static object SensitiveWordsFilter(string originaString)
        {
            if (!string.IsNullOrEmpty(originaString))
            {
                foreach (var sensitiveString in arr)
                {
                    if (originaString.Contains(sensitiveString))
                    {
                        originaString = originaString.Replace(sensitiveString, sensitiveString.Length > 1 ? "**" : "*");
                    }
                }
            }
            return originaString;
        }


        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var parameters = filterContext.ActionDescriptor.GetParameters();
            foreach (var parameter in parameters)
            {
                if (parameter.ParameterType == typeof(string))
                {
                    //get parameter oldvalue
                    var orginalValue = filterContext.ActionParameters[parameter.ParameterName] as string;
                    var filteredValue = SensitiveWordsFilter(orginalValue);
                    //set the filtered value
                    filterContext.ActionParameters[parameter.ParameterName] = filteredValue;
                }
            }
        }


    }
}