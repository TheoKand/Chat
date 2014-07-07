using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MVC_JQuery_Chat
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Chat",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Chat", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}