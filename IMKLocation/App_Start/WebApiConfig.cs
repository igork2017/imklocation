using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using IMKLocation.Services;
using Unity;
using Unity.Lifetime;

namespace IMKLocation
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //Unity implementation
            var container=new UnityContainer();
            container.RegisterType<IDataService, DataService>(new HierarchicalLifetimeManager());
        }
    }
}
