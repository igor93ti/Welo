//using System;
//using System.Reflection;
//using System.Web.Http;
//using Autofac;
//using Autofac.Integration.WebApi;
//using Microsoft.Bot.Builder.Dialogs;
//using Microsoft.Bot.Builder.Dialogs.Internals;
//using Welo.Application.Interfaces;
//using Welo.Bot.Commands;
//using Welo.IoC;

//namespace Welo.Bot.App_Start
//{
//    public class AutofacBootstrap
//    {
//        internal static void Init()
//        {
//            // http://docs.autofac.org/en/latest/integration/webapi.html#quick-start
//            var builder = new ContainerBuilder();

//            const string BotId = "WeloBot";

//            // register the Bot Builder module
//            builder.RegisterModule(new DialogModule());

//            // register some configuration
//            builder.Register(c => new BotIdResolver(BotId)).AsImplementedInterfaces().SingleInstance();
//            builder.RegisterType<StartUpCommand>().As<IDialog<object>>().InstancePerDependency();

//            builder
//              .Register<Func<IDialog<object>>>(c =>
//              {
//                  var scope = c.Resolve<ILifetimeScope>();
//                  return () => new StartUpCommand(scope.Resolve<IStandardCommandsAppService>());
//              })
//              .AsSelf()
//              .InstancePerLifetimeScope();

//            builder.RegisterModule<ApplicationModule>();
//            builder.RegisterModule<DomainModule>();
//            builder.RegisterModule<DataModule>();

//            builder.RegisterApiControllers(Assembly.GetExecutingAssembly()).InstancePerRequest();
//            var config = GlobalConfiguration.Configuration;
//            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
//            builder.RegisterWebApiFilterProvider(config);
//            var container = builder.Build();
//            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
//        }
//    }
//}