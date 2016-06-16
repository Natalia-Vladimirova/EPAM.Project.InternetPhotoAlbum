using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MvcProject
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Rating",
                url: "Home/Rate/{userName}/{page}/{photoId}/{rating}",
                defaults: new
                {
                    controller = "Home",
                    action = "Rate",
                    userName = UrlParameter.Optional,
                    page = UrlParameter.Optional,
                    photoId = UrlParameter.Optional,
                    rating = UrlParameter.Optional
                }
            );

            routes.MapRoute(
                name: "RemoveRate",
                url: "Home/RemoveRate/{userName}/{page}/{photoId}",
                defaults: new
                {
                    controller = "Home",
                    action = "RemoveRate",
                    userName = UrlParameter.Optional,
                    page = UrlParameter.Optional,
                    photoId = UrlParameter.Optional
                }
            );

            routes.MapRoute(
                name: "SearchPhotos",
                url: "Home/SearchPhotos/{userName}/{page}/{currentPhotoId}/{photoName}",
                defaults: new
                {
                    controller = "Home",
                    action = "SearchPhotos",
                    userName = UrlParameter.Optional,
                    page = UrlParameter.Optional,
                    currentPhotoId = UrlParameter.Optional,
                    photoName = UrlParameter.Optional
                }
            );

            routes.MapRoute(
                name: "Photos",
                url: "Home/Photos/{userName}/{page}/{currentPhotoId}",
                defaults: new
                {
                    controller = "Home",
                    action = "Photos",
                    userName = UrlParameter.Optional,
                    page = UrlParameter.Optional,
                    currentPhotoId = UrlParameter.Optional
                }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
