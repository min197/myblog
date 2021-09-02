using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MyBlog
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}"); //Không nhận đường dẫn đến file resource

            // BotDetect requests must not be routed
            routes.IgnoreRoute("{*botdetect}",
              new { botdetect = @"(.*)BotDetectCaptcha\.ashx" });

            routes.MapRoute(
               name: "Home Index",
               url: "trang-chu",
               defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
               namespaces: new[] { "MyBlog.Controllers" }
           );

            routes.MapRoute(
                name: "Post Detail",
                url: "blog/{metatitle}-{PostId}",
                defaults: new { controller = "Blog", action = "Detail", id = UrlParameter.Optional },
                namespaces: new[] { "MyBlog.Controllers" }
            );

            routes.MapRoute(
                name: "Tag Detail",
                url: "blog/tag/{metatitle}-{TagId}",
                defaults: new { controller = "Blog", action = "BlogTag", id = UrlParameter.Optional },
                namespaces: new[] { "MyBlog.Controllers" }
            );


            // routes.MapRoute(
            //    name: "Post Detail",
            //    url: "blog",
            //    defaults: new { controller = "Blog", action = "Index", id = UrlParameter.Optional },
            //    namespaces: new[] { "MyBlog.Controllers" }
            //);

            routes.MapRoute(
                name: "Post on Category",
                url: "danh-muc-blog/{metatitle}-{CatId}",
                defaults: new { controller = "Blog", action = "BlogCategory", id = UrlParameter.Optional },
                namespaces: new[] { "MyBlog.Controllers" }
            );

            routes.MapRoute(
               name: "Blog Index",
               url: "blog",
               defaults: new { controller = "Blog", action = "Index", id = UrlParameter.Optional },
               namespaces: new[] { "MyBlog.Controllers" }
           );

            routes.MapRoute(
               name: "Service Index",
               url: "dich-vu",
               defaults: new { controller = "Service", action = "Index", id = UrlParameter.Optional },
               namespaces: new[] { "MyBlog.Controllers" }
           );


            routes.MapRoute(
               name: "Contact Index",
               url: "lien-he",
               defaults: new { controller = "Contact", action = "Index", id = UrlParameter.Optional },
               namespaces: new[] { "MyBlog.Controllers" }
           );

            routes.MapRoute(
              name: "Forum Index",
              url: "dien-dan",
              defaults: new { controller = "Forum", action = "Index", id = UrlParameter.Optional },
              namespaces: new[] { "MyBlog.Controllers" }
          );

            routes.MapRoute(
                name: "About Index",
                url: "gioi-thieu",
                defaults: new { controller = "About", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "MyBlog.Controllers" }
            );

            routes.MapRoute(
               name: "About User",
               url: "gioi-thieu/{user}-{UserId}",
               defaults: new { controller = "About", action = "AboutUser", id = UrlParameter.Optional },
               namespaces: new[] { "MyBlog.Controllers" }
           );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "MyBlog.Controllers" }
            );

           

        }
    }
}
