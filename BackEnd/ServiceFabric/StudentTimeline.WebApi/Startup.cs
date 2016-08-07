using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.OData.Extensions;
using Owin;

namespace StudentTimeline.WebApi
{
    public static class Startup
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public static void ConfigureApp(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();

            config.AddODataQueryFilter();

            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "ByUserApi",
                routeTemplate: "api/{controller}/User/{userId}",
                defaults: new { userId = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "ByCourseApi",
                routeTemplate: "api/{controller}/Course/{courseId}",
                defaults: new { courseId = RouteParameter.Optional }
            );

            appBuilder.UseWebApi(config);
        }
    }
}
