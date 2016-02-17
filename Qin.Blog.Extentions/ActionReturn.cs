using System;
using System.Web.Mvc;

namespace Qin.Blog.Extentions
{
    public class ActionReturn : ActionResult
    {
        #region 属性
        private bool _status = false;
        private string _message = string.Empty;
        private object _data = string.Empty;
        private string _url = string.Empty;

        /// <summary>
        /// 状态(0:失败，1:成功)
        /// </summary>
        public bool Status
        {
            get { return _status; }
            set { _status = value; }
        }

        /// <summary>
        /// 返回的消息
        /// </summary>
        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        /// <summary>
        /// 返回的数据
        /// </summary>
        public object Data
        {
            get { return _data; }
            set { _data = value; }
        }

        /// <summary>
        /// 返回的URL
        /// </summary>
        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }
        #endregion


        /// <summary>
        /// 返回bool状态
        /// </summary>
        /// <param name="status"></param>
        public ActionReturn(bool status)
        {
            Status = status;
        }

        /// <summary>
        /// 返回状态和URL
        /// </summary>
        /// <param name="status"></param>
        /// <param name="url"></param>
        public ActionReturn(bool status, string url)
        {
            Status = status;
            Url = url;
        }

        /// <summary>
        /// 返回状态、提示消息和URL
        /// </summary>
        /// <param name="status"></param>
        /// <param name="message"></param>
        /// <param name="url"></param>
        public ActionReturn(bool status, string message, string url)
        {
            Status = status;
            Message = message;
            Url = url;
        }

        /// <summary>
        /// 返回请求状态、数据和提示消息
        /// </summary>
        /// <param name="data"></param>
        public ActionReturn(object data, string message)
        {
            Status = true;
            Message = message;
            Data = data;
        }

        /// <summary>
        /// 只返回数据
        /// </summary>
        /// <param name="data"></param>
        public ActionReturn(object data)
        {
            Status = true;
            Data = data;
        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="context"></param>
        public override void ExecuteResult(ControllerContext context)
        {
            var model = new
            {
                status = Status,
                message = Message,
                data = Data,
                url = Url
            };

            var result = new JsonResult
            {
                Data = model,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = 104857600  //100MB
            };

            result.ExecuteResult(context);
        }
    }
}