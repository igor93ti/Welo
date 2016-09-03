using Autofac;
using WeloBot.Application.AppServices;
using WeloBot.Application.Interfaces;
using WeloBot.Data;
using WeloBot.Domain.Interfaces.Repositories;
using WeloBot.Domain.Interfaces.Services;
using WeloBot.Domain.Services;

namespace WeloBot.Bot
{
    public class AutofacBootstrap
    {
        internal static void Init(ContainerBuilder builder)
        {
            builder.RegisterType<StandartCommandsAppService>()
                   .As<IStandartCommandsAppService>()
                   .SingleInstance();

            builder.RegisterType<StandartCommandsService>()
                   .As<IStandardCommandService>()
                   .SingleInstance();

            builder.RegisterType<StandardCommandRepository>()
                   .As<IStandardCommandRepository>()
                   .SingleInstance();
        }
    }
}