using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using WeloBot.Bot.Controllers;
using WeloBot.Bot.Maps;

namespace WeloBot.Bot
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
            builder.RegisterApiControllers(typeof(CommandsController).Assembly);
            AutofacBootstrap.Init(builder);
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}