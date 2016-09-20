using System;
using Ninject.Modules;
using Ninject.Web.Common;
using Welo.Domain.Interfaces.Services;
using Welo.Domain.Services;

namespace Welo.IoC
{
    public class DomainModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IStandardCommandService>()
                .To<StandartCommandsService>().InRequestScope();
        }
    }
}