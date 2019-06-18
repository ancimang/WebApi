using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using AppTheatre.Interfaces;
using AppTheatre.Repositories;
using AppTheatre.Resolver;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using Unity;
using Unity.Lifetime;

namespace AppTheatre
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );


            //config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling
            //                    = Newtonsoft.Json.ReferenceLoopHandling.Serialize;
            //config.Formatters.JsonFormatter.SerializerSettings.PreserveReferencesHandling
            //                    = Newtonsoft.Json.PreserveReferencesHandling.Objects;
            // CORS
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);


            // Unity
            var container = new UnityContainer();

            container.RegisterType<IGlumac, GlumacRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IVeza, VezaRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IPozoriste, PozoristeRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IPredstava, PredstavaRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IVrstaPredstave, VrstePredstaveRepository>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new UnityResolver(container);
        }
    }
}
