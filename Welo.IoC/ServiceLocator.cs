using System;
using System.Collections.Generic;
using Welo.Application.AppServices;
using Welo.Application.Interfaces;
using Welo.Data;
using Welo.Domain.Interfaces.Repositories;
using Welo.Domain.Interfaces.Services;
using Welo.Domain.Interfaces.Services.GSheets;
using Welo.Domain.Services;
using Welo.Domain.Services.GSheets;
using Welo.GoogleDocsData;

namespace Welo.IoC
{
    public class ServiceLocator : IServiceLocator
    {
        private readonly IDictionary<object, object> _services;

        public ServiceLocator()
        {
            _services = new Dictionary<object, object>();

            _services.Add(typeof(IStandardCommandRepository), new StandardCommandRepository());
            _services.Add(typeof(IGSheetsService), new GSheetsService());
            _services.Add(typeof(ICommandTextGoogle), new CommandTextGoogle(GetService<IGSheetsService>()));

            _services.Add(typeof(IStandardCommandService),
                new StandardCommandsService(
                    GetService<IStandardCommandRepository>(),
                    GetService<ICommandTextGoogle>()));

            _services.Add(typeof(IStandardCommandsAppService),
                new StandardCommandsAppService(GetService<IStandardCommandService>()));
        }

        public T GetService<T>()
        {
            try
            {
                return (T)_services[typeof(T)];
            }
            catch (KeyNotFoundException)
            {
                throw new ApplicationException("The requested service is not registered");
            }
        }
    }
}