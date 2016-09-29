using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Example;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Util.Store;
using Newtonsoft.Json;
using Welo.Domain.Entities;
using Welo.Domain.Interfaces.Services.GSheets;

namespace Welo.GoogleDocsData
{
    public class GSheetsService : IGSheetsService
    {
        private static readonly string[] Scopes = { SheetsService.Scope.Spreadsheets };
        private SheetsService _sheetsService = null;
        public GSheetContext Context { get; set; }

        public GSheetsService()
        {
            var appDomain = AppDomain.CurrentDomain;
            var basePath = appDomain.BaseDirectory;

            var pathDirectory = Path.Combine(basePath, "App_Data");
            Context = new GSheetContext
            {
                ApplicationName = "WeloBot",
                SpreadsheetId = "1TxL93syBaHLZnrXj_Ll8eTJ0O1hCx2iwJfJdqXtZUsU",
                PathConfig = pathDirectory,
                NameFile = "client_secret.json",
                User = "welobot@welobotv2.iam.gserviceaccount.com"
            };
        }

        private SheetsService Service
        {
            get
            {

                if (_sheetsService != null)
                    return _sheetsService;
                

                var pathFile = Path.Combine(Context.PathConfig, Context.NameFile);
                // Get active credential

                var json = File.ReadAllText(pathFile);
                var cr = JsonConvert.DeserializeObject<PersonalServiceAccountCred>(json); // "personal" service account credential

                // Create an explicit ServiceAccountCredential credential
                var xCred = new ServiceAccountCredential(new ServiceAccountCredential.Initializer(cr.ClientEmail)
                {
                    Scopes = Scopes
                }.FromPrivateKey(cr.PrivateKey));
                
                _sheetsService = new SheetsService(new BaseClientService.Initializer
                {
                    HttpClientInitializer = xCred,
                    ApplicationName = Context.ApplicationName
                });

                return _sheetsService;
            }
        }

        public IList<IList<object>> BatchGet(string[] ranges)
        {
            try
            {
                var request = Service.Spreadsheets.Values.BatchGet(Context.SpreadsheetId);
                request.MajorDimension = SpreadsheetsResource.ValuesResource.BatchGetRequest.MajorDimensionEnum.ROWS;
                request.Ranges = ranges;
                var response = request.Execute();
                return response.ValueRanges.SelectMany(x => x.Values).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}