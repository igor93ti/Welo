using System;
using Autofac;
using Welo.Application.AppServices;
using Welo.Application.Interfaces;

namespace Welo.IoC
{
    public class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            builder.RegisterType<StandartCommandsAppService>()
                      .As<IStandartCommandsAppService>()
                      .InstancePerRequest();
        }
    }
}