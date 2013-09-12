using Autofac;
using Autofac.Integration.WebApi;
using AutoMapper;
using ShopBackEnd.Business;
using ShopBackEnd.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;


namespace ShopBackEnd.MvcApplication.DependencyContainer
{
    public class DependencyResolverfactory
    {
        public static void ResolveDependency(HttpConfiguration configuration)
        {
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerApiRequest();

            //builder.RegisterType<ProductService>().As(typeof(IProductService));

            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
           .Where(t => t.Name.EndsWith("Service"))
           .AsImplementedInterfaces();
            
            var container = builder.Build();
            var resolver = new AutofacWebApiDependencyResolver(container);
            configuration.DependencyResolver = resolver;

            InitializeMapperProfiles();
        }

        private static void InitializeMapperProfiles()
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
    }
}