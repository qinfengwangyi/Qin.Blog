using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Qin.Blog.Web.Deal
{
    /// <summary>
    /// UploadImage 的摘要说明
    /// </summary>
    public class UploadImage : IHttpHandler
    {
        ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private HttpRequest Request { get; set; }
        private HttpResponse Response { get; set; }
        private HttpServerUtility Server { get; set; }

        private string ServerBaseURL = ConfigurationManager.AppSettings["UploadImage"].ToString()
            + DateTime.Now.ToString("yyyyMMdd") + "/";

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                this.Request = context.Request;
                this.Response = context.Response;
                this.Server = context.Server;

                var file = Request.Files["file"];//get picture
                string fileName = file.FileName;
                int fileLength = file.ContentLength;
                string fileExtension = Path.GetExtension(Path.GetFileName(file.FileName));

                context.Response.ContentType = "text/plain";
                if (fileLength > 512000)
                {
                    string jsonerror = "{'result':'false','msg':'file too big！'}";
                    Response.Write(jsonerror);
                }
                string newFileName = Guid.NewGuid().ToString("N") + fileExtension;
                string filePath = Path.Combine(Server.MapPath(ServerBaseURL), newFileName);

                if (!Directory.Exists(Server.MapPath(ServerBaseURL)))
                {//create file path
                    Directory.CreateDirectory(Server.MapPath(ServerBaseURL));
                }
                file.SaveAs(filePath);

                string json = "{result:'true',pics:'" + ServerBaseURL + newFileName + "'}";
                context.Response.Write(json);
            }
            catch (Exception e)
            {
                string error = string.Format("发生异常页: UploadImage-> ProcessRequest；异常信息: {0}", e.Message);
                log.Error(error);
                context.Response.Write(error);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}