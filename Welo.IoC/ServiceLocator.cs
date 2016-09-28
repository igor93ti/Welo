using System;
using System.Collections.Generic;

namespace Welo.IoC
{
    public class ServiceLocator : IServiceLocator
    {
        private readonly IDictionary<object, object> _services;

        public ServiceLocator()
        {
            //var appDomain = AppDomain.CurrentDomain;
            //var basePath = appDomain.BaseDirectory;

            //var pathDirectory = Path.Combine(basePath, "App_Data");

            //var gSheetContext = new GSheetContext
            //{
            //    ApplicationName = "WeloBot",
            //    SpreadsheetId = "1TxL93syBaHLZnrXj_Ll8eTJ0O1hCx2iwJfJdqXtZUsU",
            //    PathConfig = pathDirectory,
            //    NameFile = "client_secret.json",
            //    User = "eugenio00"
            //};

            //var gSheetsService = new GSheetsService();
            //gSheetsService.Context = gSheetContext;

            //_services = new Dictionary<object, object>();

            //_services.Add(typeof(IGSheetsService), gSheetsService);
            //_services.Add(typeof(IStandardCommandRepository), new StandardCommandRepository());
            //_services.Add(typeof(ICommandTextGoogle), new CommandTextGoogle(GetService<IGSheetsService>()));

            //_services.Add(typeof(IStandardCommandService),
            //    new StandardCommandsService(GetService<IStandardCommandRepository>(), GetService<ICommandTextGoogle>()));

            //_services.Add(typeof(IStandardCommandsAppService),
            //    new StandardCommandsAppService(GetService<IStandardCommandService>()));
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