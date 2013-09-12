using Autofac;
using Autofac.Integration.WebApi;
using AutoMapper;
using ShopBackEnd.Business;
using ShopBackEnd.Common;
using ShopBackEnd.MvcApplication.Controllers;
using ShopBackEnd.MvcApplication.Filters;
using ShopBackEnd.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;


namespace ShopBackEnd.MvcApplication.DependencyContainer
{
    public static class DependencyResolverfactory
    {
        public static void ResolveDependency(HttpConfiguration configuration)
        {
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerApiRequest();

            //builder.RegisterType<ProductService>().As(typeof(IProductService));

            // Register Filters
            RegisterUnhandledExceptionFilter(builder);

            // Register Automapper engine
            builder.Register(a => Mapper.Engine).As<IMappingEngine>().InstancePerLifetimeScope();
            builder.RegisterType<LogService>().As<ILogService>().InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
           .Where(t => t.Name.EndsWith("Service"))
           .AsImplementedInterfaces();

            var container = builder.Build();
            var resolver = new AutofacWebApiDependencyResolver(container);
            configuration.DependencyResolver = resolver;

            InitializeMapperProfiles();
        }

        public static void InitializeMapperProfiles()
        {
            Mapper.Initialize(x => GetConfiguration(Mapper.Configuration));
        }

        private static void GetConfiguration(IConfiguration configuration)
        {
            var profileType = typeof(Profile);

            var profiles = Assembly.GetAssembly(typeof(IService<>)).GetTypes()
            .Where(t => profileType.IsAssignableFrom(t)
                && t.GetConstructor(Type.EmptyTypes) != null)
            .Select(Activator.CreateInstance)
            .Cast<Profile>().ToList();

            Mapper.Initialize(a => profiles.ForEach(a.AddProfile));

        }

        private static void RegisterUnhandledExceptionFilter(ContainerBuilder builder)
        {
            builder.Register(c => new UnhandledExceptionFilter(c.Resolve<ILogService>()))
                .AsWebApiExceptionFilterFor<ProductsController>()
                .InstancePerApiRequest();

        }
    }
}