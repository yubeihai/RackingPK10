using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Web.Mvc;
using System.Web.Routing;

namespace RacingPK10
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            //ModelBinders.Binders.Add(typeof(JObject), new JObjectModelBinder());
            //ModelBinders.Binders.Add(typeof(Array), new JObjectModelBinder());
            //routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
        //public class JObjectModelBinder : IModelBinder
        //{
        //    public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        //    {
        //        var stream = controllerContext.RequestContext.HttpContext.Request.InputStream;
        //        stream.Seek(0, SeekOrigin.Begin);
        //        string json = new StreamReader(stream).ReadToEnd();
        //        return JsonConvert.DeserializeObject<dynamic>(json);
        //    }
        //}
    }
}
