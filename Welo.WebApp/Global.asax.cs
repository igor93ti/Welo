
using System;
using Autofac;
using Autofac.Integration.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Welo.IoC;
using Welo.WebApp.Mappers;

namespace Welo.WebApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public object Conversation { get; private set; }

        protected void Application_Start()
        {
            RegisterWebAppDependencies();
            AutoMapper.Mapper.Initialize(cfg => cfg.AddProfile<AutoMapperConfig>());
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void RegisterWebAppDependencies()
        {
            var builder = new ContainerBuilder();

            //builder.RegisterModule<ApplicationModule>();

            builder.RegisterModule<DataModule>();


            builder.RegisterModule<DomainModule>();
            builder.RegisterModule<WebAppModule>();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            IContainer container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
