using System;
using Ninject.Modules;
using Ninject.Web.Common;
using Welo.Data;
using Welo.Domain.Interfaces.Repositories;

namespace Welo.IoC
{
    public class DataModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IStandardCommandRepository>()
                .To<StandardCommandRepository>().InRequestScope();
        }
    }
}