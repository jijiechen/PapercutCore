using System.Web.Http;

namespace Papercut.Module.WebUI
{
    using System.Net.Http;
    using System.Web.Http.Routing;

    using Autofac;
    using Autofac.Integration.WebApi;

    public static class RouteConfig
    {
        public static void Init(HttpConfiguration config, ILifetimeScope scope)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(scope);

            config.Routes.MapHttpRoute("health", "health", new {controller = "Health"});

            config.Routes.MapHttpRoute("load all messages",
                "api/messages",
                new {controller = "Messages", action = "GetAll"},
                new { HttpMethod = new HttpMethodConstraint(HttpMethod.Get)});

            config.Routes.MapHttpRoute("delete all messages",
                "api/messages",
                new { controller = "Messages", action = "DeleteAll" },
                new { HttpMethod = new HttpMethodConstraint(HttpMethod.Delete) });

            config.Routes.MapHttpRoute("load message detail",
                "api/messages/{id}",
                new {controller = "Messages", action = "Get"});

            config.Routes.MapHttpRoute("download section by content id",
                "api/messages/{messageId}/contents/{contentId}",
                new {controller = "Messages", action = "DownloadSectionContent" });

            config.Routes.MapHttpRoute("download section by index",
                "api/messages/{messageId}/sections/{index}",
                new {controller = "Messages", action = "DownloadSection" });

            config.Routes.MapHttpRoute("download raw message palyload",
                "api/messages/{messageId}/raw",
                new { controller = "Messages", action = "DownloadRaw" });

            config.Routes.MapHttpRoute("Serve other requests as static content",
                "{*anything}",
                new {controller = "StaticContent", anything = RouteParameter.Optional});
        }
    }
}