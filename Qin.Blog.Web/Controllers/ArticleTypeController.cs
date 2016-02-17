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
    public class ArticleTypeController : BaseController
    {
        ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        IArticleTypeService _IArticleTypeService = new ArticleTypeService();


        /// <summary>
        /// 全局默认文章分类
        /// </summary>
        /// <returns></returns>
        public ActionResult TypeList()
        {
            try
            {
                int total = 0;
                List<ArticleType> list = _IArticleTypeService.GetList(out total);
                if (list != null && list.Count > 0)
                {
                    return new ActionReturn(list);
                }
                return new ActionReturn(false);
            }
            catch (Exception e)
            {
                string error = string.Format("发生了错误，错误页：{0}，错误信息{1}", Request.Url.ToString(), e.Message.ToString());
                log.Error(error);
                return new ActionReturn(false);
            }
        }


        /// <summary>
        /// 添加类型名称
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="typeMark"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        [LoginAuthorize]
        public ActionResult Insert(string typeName, string mark)
        {
            try
            {
                ArticleType model = new ArticleType
                {
                    Id = Guid.NewGuid().ToString("N"),
                    TypeName = typeName,
                    Mark = mark,
                    CreateUser = CUR_USER.UserName,
                    CreateTime = DateTime.Now
                };
                if (_IArticleTypeService.Insert(model))
                {
                    return AlertMsg("添加成功！", Url.Action("AddArticle", "Home"));
                }
                else
                {
                    return AlertMsgAndJs("添加失败！", "history.go(-1);");
                }

            }
            catch (Exception e)
            {
                string error = string.Format("发生异常页: {0}；异常信息: {1}", Request.Url, e.Message);
                log.Error(error);
                _ISysLogService.Insert(new SysLog()
                {
                    Location = e.Source.ToString(),
                    Content = "e.StackTrace:" + e.StackTrace + ",e.Data:" + e.Data,
                    Error = e.Message,
                    CreateUser = Session["UserName"] != null ? Session["UserName"].ToString() : "无用户名",
                    RemoteHost = Request.UserHostAddress + ":" + Request.UserHostName,
                    RequestData = Request.Params.ToString(),
                    Browser = Request.Browser.Browser + Request.Browser.Version,
                    ActionCode = Request.RawUrl,
                });
                return AlertMsgAndJs("添加失败！", "history.go(-1);");
            }
        }


    }
}