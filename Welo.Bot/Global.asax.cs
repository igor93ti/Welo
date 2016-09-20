using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Ninject;
using Welo.Bot.Maps;

namespace Welo.Bot
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AutoMapperConfig.RegisterMappings();
        }
    }
}