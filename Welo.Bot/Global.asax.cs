using System;
using System.Configuration;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Internals.Fibers;
using Quartz;
using Quartz.Impl;
using RollbarDotNet;
using Welo.Application.Maps;
using Welo.Bot.App_Start;
using Welo.Bot.Commands;
using Welo.Bot.Commands.Interfaces;
using Welo.Bot.Filters;
using Welo.IoC;

namespace Welo.Bot
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            RegisterBotDependencies();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            AutoMapperConfig.RegisterMappings();
            Rollbar.Init(new RollbarConfig
            {
                AccessToken = ConfigurationManager.AppSettings["Rollbar.AccessToken"],
                Environment = ConfigurationManager.AppSettings["Rollbar.Environment"]
            });

            ScheduleJobs();
        }

        private void RegisterBotDependencies()
        {
            var builder = new ContainerBuilder();
            
            builder.RegisterModule<ApplicationModule>();
            builder.RegisterModule<DataModule>();
            builder.RegisterModule<DomainModule>();

            builder.RegisterType<RootDialog>().As<IRootDialog>().InstancePerDependency();
            builder.RegisterType<HelpCommand>().As<IHelpCommand>().InstancePerLifetimeScope();
            builder.RegisterType<RandomCommand>().As<IRandomCommand>().InstancePerLifetimeScope();
            builder.RegisterType<StartUpCommand>().As<IStartUpCommand>().InstancePerLifetimeScope();
            builder.RegisterType<SubscribeCommand>().As<ISubscribeCommand>().InstancePerLifetimeScope();
            builder.RegisterModule(new ReflectionSurrogateModule());

            builder.RegisterControllers(typeof(WebApiApplication).Assembly);

            builder.Update(Conversation.Container);

            DependencyResolver.SetResolver(new AutofacDependencyResolver(Conversation.Container));
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var exception = Server.GetLastError().GetBaseException();
            Rollbar.Report(exception);
        }

        private static void ScheduleJobs()
        {
            try
            {
                // construct a scheduler factory
                ISchedulerFactory schedFact = new StdSchedulerFactory();

                // get a scheduler
                var sched = schedFact.GetScheduler();
                sched.Start();

                var job = JobBuilder.Create<SubscriberJob>()
                    .WithIdentity("myJob", "group1")
                    .Build();

                var trigger = TriggerBuilder.Create()
                    //.WithDailyTimeIntervalSchedule
                    //(s =>
                    //    s.WithIntervalInSeconds(10)
                    //        .OnEveryDay()
                    //        .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(10, 15))
                    //)
                    .WithSimpleSchedule(x => x
                        .WithIntervalInSeconds(10)
                        .RepeatForever())
                    .Build();

                sched.ScheduleJob(job, trigger);
            }
            catch (ArgumentException e)
            {
                throw;
            }
        }

        public class FilterConfig
        {
            public static void RegisterGlobalFilters(GlobalFilterCollection filters)
            {
                filters.Add(new RollbarExceptionFilter());
            }
        }
    }
}