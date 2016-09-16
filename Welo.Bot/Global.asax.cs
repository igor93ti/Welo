using System.Web.Http;
using Autofac;
using Welo.Bot.Maps;

namespace Welo.Bot
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RegisterIOC();
            AutoMapperConfig.RegisterMappings();
        }

        private void RegisterIOC()
        {
            var builder = new ContainerBuilder();
            AutofacBootstrap.Init(builder);
        }
    }
}