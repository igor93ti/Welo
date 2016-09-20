using System;
using Ninject.Modules;
using Ninject;
using Ninject.Web.Common;
using Welo.Bot.Commands;

namespace Welo.Bot.App_Start
{
    public class BotModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IStartUpCommand>()
                .To<StartUpCommand>().InRequestScope();
        }
    }
}