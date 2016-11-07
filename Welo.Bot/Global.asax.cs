using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.WebApi;
using Common.Logging;
using Microsoft.IdentityModel.Protocols;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Quartz;
using Quartz.Impl;
using RollbarDotNet;
using Welo.Bot.App_Start;
using Welo.Bot.Commands;
using Welo.Bot.Filters;
using Welo.Bot.Maps;
using Welo.Domain.Entities;
using System.Collections.Generic;
using System.Web;
using Welo.Application.AppServices;

namespace Welo.Bot
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AutoMapperConfig.RegisterMappings();
            Rollbar.Init(new RollbarConfig
            {
                AccessToken = ConfigurationManager.AppSettings["Rollbar.AccessToken"],
                Environment = ConfigurationManager.AppSettings["Rollbar.Environment"]
            });
            AutofacBootstrap.Init();
            string path = Server.MapPath("~/datafile/commands.json");

            StandardCommandsAppService.Intance.Init();

            ScheduleJobs();
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
                        .WithIntervalInSeconds(40)
                        .RepeatForever())
                    .Build();

                sched.ScheduleJob(job, trigger);
            }
            catch (ArgumentException e)
            {
                throw;
            }
        }

        public static ILifetimeScope FindContainer()
        {
            var config = GlobalConfiguration.Configuration;
            var resolver = (AutofacWebApiDependencyResolver)config.DependencyResolver;
            return resolver.Container;
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