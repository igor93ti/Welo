using Welo.Domain.Interfaces;

namespace Welo.GoogleDocsData
{
    public class GSheetContext : IGSheetContext
    {
        public GSheetContext()
        {
        }

        public string ApplicationName { get; set; }
        public string SpreadsheetId { get; set; }
        public string PathConfig { get; set; }
        public string NameFile { get; set; }
        public string User { get; set; }
    }
}