using System;
using Autofac;
using Welo.Application.AppServices;
using Welo.Application.Interfaces;
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
                      .InstancePerLifetimeScope();

            builder.RegisterType<LeadAppService>()
                      .As<ILeadAppService>()
                      .InstancePerLifetimeScope();

            builder.RegisterType<GSheetsService>().As<IGSheetsService>().InstancePerLifetimeScope();
            builder.RegisterType<CommandTextGoogle>().As<ICommandTextGoogle>().InstancePerLifetimeScope();
        }
    }
}