namespace Welo.Domain.Interfaces
{
    public interface IGSheetContext
    {
        string ApplicationName { get; set; }
        string NameFile { get; set; }
        string PathConfig { get; set; }
        string SpreadsheetId { get; set; }
        string User { get; set; }
    }
}