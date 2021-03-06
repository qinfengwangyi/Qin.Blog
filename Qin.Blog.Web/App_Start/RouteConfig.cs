﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Qin.Blog.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //文章详细页
            routes.MapRoute(
                name: "Article/Detail",
                url: "article/{id}.html",
                defaults: new { controller = "Article", action = "Detail", id = UrlParameter.Optional },
                namespaces: new[] { "Qin.Blog.Web.Controllers" }
            );

            //Edit Article
            routes.MapRoute(
                name: "Article/Edit",
                url: "editarticle/{id}.html",
                defaults: new { controller = "Article", action = "Edit", id = UrlParameter.Optional },
                namespaces: new[] { "Qin.Blog.Web.Controllers" }
            );


            //登录
            routes.MapRoute(
                name: "account/login",
                url: "login",
                defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional },
                namespaces: new[] { "Qin.Blog.Web.Controllers" }
            );


            //注册
            routes.MapRoute(
                name: "account/register",
                url: "register",
                defaults: new { controller = "Account", action = "Register", id = UrlParameter.Optional },
                namespaces: new[] { "Qin.Blog.Web.Controllers" }
            );

            //留言
            routes.MapRoute(
                name: "leavemessage",
                url: "leavemsg",
                defaults: new { controller = "LeaveMessage", action ="Index", id = UrlParameter.Optional},
                namespaces: new []{"Qin.Blog.Web.Controllers"}
            );

            //tag category
            routes.MapRoute(
                name: "Article/TagPages",
                url: "tagarticle",
                defaults: new { controller = "Article", action = "TagPages", id = UrlParameter.Optional },
                namespaces: new[] { "Qin.Blog.Web.Controllers" }
            );


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "Qin.Blog.Web.Controllers" }
            );

        }
    }
}
