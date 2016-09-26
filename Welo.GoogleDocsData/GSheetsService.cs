using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Util.Store;
using Welo.Domain.Entities;
using Welo.Domain.Interfaces.Services.GSheets;

namespace Welo.GoogleDocsData
{
    public class GSheetsService : IGSheetsService
    {
        private static readonly string[] Scopes = { SheetsService.Scope.Spreadsheets };
        public GSheetContext Context { get; set; }
        private SheetsService _sheetsService = null;
        
        private SheetsService Service
        {
            get
            {
                if (_sheetsService != null)
                    return _sheetsService;

                UserCredential credential;
                var pathFile = Path.Combine(Context.PathConfig, Context.NameFile);
                using (var stream = new FileStream(pathFile, FileMode.Open, FileAccess.Read))
                {
                    var pathCred = Path.Combine(Context.PathConfig, "credentials.json");
                    credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.Load(stream).Secrets,
                        Scopes,
                        Context.User,
                        CancellationToken.None,
                        new FileDataStore(pathCred, true)).Result;
                }

                _sheetsService = new SheetsService(new BaseClientService.Initializer
                {
                    HttpClientInitializer = credential,
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
