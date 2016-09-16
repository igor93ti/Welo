using System;
using Autofac;
using Welo.Domain.Interfaces.Repositories;

namespace Welo.Data
{
    public class DataModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            builder.RegisterType<StandardCommandRepository>()
                   .As<IStandardCommandRepository>()
                   .InstancePerRequest();
        }
    }
}