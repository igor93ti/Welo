using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Util;
using Google.Apis.Util.Store;

namespace Welo.Application.AppServices
{
    public static class GoogleService
    {
        static string[] Scopes = { SheetsService.Scope.SpreadsheetsReadonly };
        static string ApplicationName = "WeloBot";
        static string spreadsheetId = "1TxL93syBaHLZnrXj_Ll8eTJ0O1hCx2iwJfJdqXtZUsU";
        static string pathConfig = "\\\\Mac\\Home\\Documents";
        static string nameFile = "client_secret.json";
        static string user = "eugenio00";

        public static IList<ValueRange> GetValues(string[] ranges)
        {
            try
            {
                UserCredential credential;
                var pathFile = Path.Combine(pathConfig, nameFile);
                using (var stream = new FileStream(pathFile, FileMode.Open, FileAccess.Read))
                {
                    var pathCred = Path.Combine(pathConfig, "credentials.json");
                    credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                                            GoogleClientSecrets.Load(stream).Secrets,
                                            Scopes,
                                            user,
                                            CancellationToken.None,
                                            new FileDataStore(pathCred, true)).Result;
                }

                var service = new SheetsService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = ApplicationName,
                });

                var request = service.Spreadsheets.Values.BatchGet(spreadsheetId);
                request.MajorDimension = SpreadsheetsResource.ValuesResource.BatchGetRequest.MajorDimensionEnum.ROWS;
                request.Ranges = ranges;

                var response = request.Execute();
                return response.ValueRanges;

            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
