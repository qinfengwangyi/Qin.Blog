using Qin.Blog.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Qin.Blog.Web.App_Start
{
    public class Log4NetConfig
    {
        public static void RegisterLog4Net()
        {
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new FileInfo(RequestHelper.GetWebRootDirectory("~/log.config")));
        }
    }
}