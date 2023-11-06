using System.Web.Http;
using Unity;
using Unity.WebApi;
using BeerRatings.Core.Interfaces;
using BeerRatings.Application.Services;
using BeerRatings.Application.Interfaces;
using BeerRatings.Application.Common;
using Unity.Lifetime;
using BeerRatings.Application.Wrapper;
using BeerRatings.Infrastracture.Repositories;
using Serilog;
using Serilog.Core;
using Unity.Injection;
using System.Configuration;

namespace BeerRatings.Api.App_Start
{
    public class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // Register your services and other dependencies
            container.RegisterType<IBeerRatingsService, BeerRatingsService>(new HierarchicalLifetimeManager());
            container.RegisterType<IBeerRatingsRepository, BeerRatingsRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ICommonHttpClientService, CommonHttpClientService>(new HierarchicalLifetimeManager());
            container.RegisterType<IPunkAPIWrapper, PunkAPIWrapper>(new HierarchicalLifetimeManager());
            container.RegisterType<ILogger>(new ContainerControlledLifetimeManager(), new InjectionFactory((ctr, type, name) =>
            {
                ILogger log = new LoggerConfiguration()
                    .WriteTo.Console() // Log to the console
                    .WriteTo.File(ConfigurationManager.AppSettings["LogFilePath"]) // Log to a file
                    .CreateLogger();
                return log;
            }));
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);

        }
    }
}