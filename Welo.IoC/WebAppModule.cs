using System;
using Autofac;
using Welo.Domain.Interfaces.Services;
using Welo.Domain.Interfaces.Services.WebApp;
using Welo.Domain.Services;
using Welo.Domain.Services.WebApp;
using Module = Autofac.Module;

namespace Welo.IoC
{
    public class WebAppModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));
            
            builder.RegisterType<MovieService>()
                   .As<IMovieService>()
                   .InstancePerLifetimeScope();
            
        }
    }
}
