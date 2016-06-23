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
                name: "Rate",
                url: "Rating/Rate/{userName}/{page}/{photoId}/{rating}",
                defaults: new
                {
                    controller = "Rating",
                    action = "Rate",
                    userName = UrlParameter.Optional,
                    page = UrlParameter.Optional,
                    photoId = UrlParameter.Optional,
                    rating = UrlParameter.Optional
                }
            );

            routes.MapRoute(
                name: "RemoveRate",
                url: "Rating/RemoveRate/{userName}/{page}/{photoId}",
                defaults: new
                {
                    controller = "Rating",
                    action = "RemoveRate",
                    userName = UrlParameter.Optional,
                    page = UrlParameter.Optional,
                    photoId = UrlParameter.Optional
                }
            );

            routes.MapRoute(
                name: "SearchPhotos",
                url: "Photo/SearchPhotos/{userName}/{page}/{currentPhotoId}/{photoName}",
                defaults: new
                {
                    controller = "Photo",
                    action = "SearchPhotos",
                    userName = UrlParameter.Optional,
                    page = UrlParameter.Optional,
                    currentPhotoId = UrlParameter.Optional,
                    photoName = UrlParameter.Optional
                }
            );

            routes.MapRoute(
                name: "Photos",
                url: "Photo/Photos/{userName}/{page}/{currentPhotoId}",
                defaults: new
                {
                    controller = "Photo",
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

            routes.MapRoute(
                name: "404-catch-all",
                url: "{*catchall}",
                defaults: new
                {
                    Controller = "Error",
                    Action = "NotFound"
                }
            );
        }
    }
}
