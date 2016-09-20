using System;
using Ninject.Modules;
using Ninject.Web.Common;
using Welo.Application.AppServices;
using Welo.Application.Interfaces;

namespace Welo.IoC
{
    public class ApplicationModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IStandartCommandsAppService>()
                .To<StandartCommandsAppService>().InRequestScope();
        }
    }
}