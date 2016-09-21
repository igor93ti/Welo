using System.Web.Mvc;
using Ninject.Web.Mvc.FilterBindingSyntax;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Welo.Bot.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Welo.Bot.App_Start.NinjectWebCommon), "Stop")]

namespace Welo.Bot.App_Start
{
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Web.Common;
    using System;
    using System.Web;
    using IoC;
    public static class NinjectWebCommon
    {
        public static readonly Bootstrapper bootstrapper = new Bootstrapper();
        public static StandardKernel Kernel { get; private set; }

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            Kernel = new StandardKernel(new NinjectSettings() { AllowNullInjection = true });
            Kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            Kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

            RegisterServices(Kernel);
            return Kernel;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            //kernel.Load<BotModule>();
            //kernel.Load<ApplicationModule>();
            //kernel.Load<DomainModule>();
            //kernel.Load<DataModule>();
        }
    }
}