using Welo.Domain.Entities;

namespace Welo.Domain.Services.GSheets
{
    public interface ICommandTextGoogle
    {
        string GetTextRandomRowGSheets(GSheetQuery range);
    }
}