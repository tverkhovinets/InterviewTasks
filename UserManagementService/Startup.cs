using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Owin;
using Microsoft.Owin;
using System.Web.Http;
using Autofac;
using System.Reflection;
using Autofac.Integration.WebApi;
using UserManagementService.Filters;
using UserManagementService.Controllers;
using UserManagementService.Model;
using UserManagementService.DataAccess;

//[assembly: OwinStartup(typeof(UserManagementService.App_Start.Startup))]
namespace UserManagementService
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var config = new HttpConfiguration();
            var builder = new ContainerBuilder();

            RegisterRoutes(config);
            ConfigureFormatters(config);

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterWebApiFilterProvider(config);
            RegisterTypes(builder);
            RegisterFilters(config);

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            appBuilder.UseAutofacMiddleware(container);
            appBuilder.UseAutofacWebApi(config);
            appBuilder.UseWebApi(config);
        }

        private void ConfigureFormatters(HttpConfiguration config)
        {
            config.Formatters.JsonFormatter.SerializerSettings.PreserveReferencesHandling =
                Newtonsoft.Json.PreserveReferencesHandling.All;
        }

        private void RegisterFilters(HttpConfiguration config)
        {
            config.Filters.Add(new UnhandledExceptionFilter());
        }

        private static void RegisterTypes(ContainerBuilder builder)
        {
            builder.Register(c => new UnitOfWork())
               .As<IUnitOfWork>()
               .InstancePerRequest();

            builder.Register(c => new DefaultCompanyActionFilter(c.Resolve<IUnitOfWork>()))
                .AsWebApiActionFilterFor<UsersController>(c => c.CreateUser(default(User)))
                .InstancePerRequest();
            builder.Register(c => new ValidationModelAttribute())
                .AsWebApiActionFilterFor<UsersController>(c => c.CreateUser(default(User)))
                .InstancePerRequest();

        }

        private static void RegisterRoutes(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}