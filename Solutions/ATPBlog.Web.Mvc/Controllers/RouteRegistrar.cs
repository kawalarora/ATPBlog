namespace ATPBlog.Web.Mvc.Controllers
{
    using System.Web.Mvc;
    using System.Web.Routing;

    public class RouteRegistrar
    {
        public static void RegisterRoutesTo(RouteCollection routes) 
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.ico(/.*)?" });

            routes.MapRoute(
                "Default",                                              // Route name
                "{controller}/{action}/{id}",                           // URL with parameters
                new { controller = "Blogs", action = "Index", id = UrlParameter.Optional }); // Parameter defaults
        }
    }
}
