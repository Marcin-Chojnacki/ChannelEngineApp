using System.Configuration;
using System.Net.Http;
using System.Reflection;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using CeApp.ApiDataAccess;
using CeApp.ApiDataAccess.Providers;
using CeApp.DataAccess;
using CeApp.Services.Order;
using CeApp.Services.Product;

namespace CeApp.DI
{
    public static class DiConfig
    {
        private static IContainer _container;

        public static void RegisterTypesForMvc(Assembly mvcAssembly)
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(mvcAssembly);
            RegisterTypes(builder);

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            _container = container;
        }

        public static void RegisterTypesForCui()
        {
            var builder = new ContainerBuilder();

            RegisterTypes(builder);

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            _container = container;
        }

        public static IContainer GetContainer() => _container;

        private static void RegisterTypes(ContainerBuilder builder)
        {
            builder.RegisterType<OrderProvider>().As<IOrderProvider>().SingleInstance();
            builder.RegisterType<ProductProvider>().As<IProductProvider>().SingleInstance();
            builder.RegisterType<OfferProvider>().As<IOfferProvider>().SingleInstance();
            builder.RegisterType<ApiConfig>().As<IApiConfig>().SingleInstance();

            builder.RegisterType<OrderService>().As<IOrderService>();
            builder.RegisterType<ProductService>().As<IProductService>();

            builder.RegisterType<HttpClientHandler>().As<HttpMessageHandler>().AsSelf();
            builder.RegisterType<HttpClient>().UsingConstructor(typeof(HttpMessageHandler));
        }
    }
}
