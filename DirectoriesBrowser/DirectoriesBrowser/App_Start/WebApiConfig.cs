using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace DirectoriesBrowser
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Конфигурация и службы веб-API

            // Маршруты веб-API
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
            name: "BookRoute",
            routeTemplate: "api/{controller}/{action}"
        );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{path}",
                defaults: new { controller = "Directories", path = RouteParameter.Optional }
            );
        }
    }
}
