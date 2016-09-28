using System;
using Autofac;
using Welo.Application.AppServices;
using Welo.Application.Interfaces;
using Welo.Domain.Entities;
using Welo.Domain.Interfaces.Services.GSheets;
using Welo.Domain.Services.GSheets;
using Welo.GoogleDocsData;

namespace Welo.IoC
{
    public class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            builder.RegisterType<StandardCommandsAppService>()
                      .As<IStandardCommandsAppService>()
                      .InstancePerRequest();

            builder.RegisterType<GSheetsService>().As<IGSheetsService>().InstancePerRequest();
            builder.RegisterType<CommandTextGoogle>().As<ICommandTextGoogle>().InstancePerRequest();
        }
    }
}