using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Welo.Bot.Maps;

namespace Welo.Bot
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AutoMapperConfig.RegisterMappings();

            //AutofacBootstrap.Init();
        }

        public static ILifetimeScope FindContainer()
        {
            var config = GlobalConfiguration.Configuration;
            var resolver = (AutofacWebApiDependencyResolver)config.DependencyResolver;
            return resolver.Container;
        }
    }
}