using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MVC5Course {
    public class RouteConfig {
        public static void RegisterRoutes(RouteCollection routes) {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //加上限制條件
            //routes.MapRoute(
            //    name: "id_con",
            //    url: "{controller}/{action}/{id}",
            //                //url: "{controller}/{action}.aspx/{id}",
            //                //url: "{controller}/{action}.php/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
            //    constraints: new { 
            //        id = @"\d+"
            //    }
            //);

            /*
             * 配合day3.txt筆記
             {*id} 比對所有id
            */
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                //url: "{controller}/{action}.aspx/{id}",
                //url: "{controller}/{action}.php/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
