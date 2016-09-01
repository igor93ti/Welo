using Autofac;
using WeloBot.Application.AppServices;
using WeloBot.Application.Interfaces;
using WeloBot.Data;
using WeloBot.Domain.Interfaces.Repositories;
using WeloBot.Domain.Interfaces.Services;
using WeloBot.Domain.Services;

namespace WeloBot.Api
{
    public class AutofacBootstrap
    {
        internal static void Init(ContainerBuilder builder)
        {
            builder.RegisterType<StandartCommandsAppService>()
                   .As<IStandardCommandAppService>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<StandartCommandsService>()
                   .As<IStandardCommandService>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<StandardCommandRepository>()
                   .As<IStandardCommandRepository>()
                   .InstancePerLifetimeScope();
        }
    }
}