using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace lua
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
            //    namespaces: new[] { "lua.Controllers" }
            //);
            //https://stackoverflow.com/questions/7781310/asp-net-mvc3-area-controller-accessible-from-global-routes
            routes.Add(
                "Default",
                new Route("{controller}/{action}/{id}",
                    new RouteValueDictionary(
                        new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                    ),
                    null,
                    new RouteValueDictionary(
                        new
                        {
                            Namespaces = new string[] { "lua.Controllers" },
                            UseNamespaceFallback = false
                        }
                    ),
                    new MvcRouteHandler()
                )
            );

        }
    }
}
