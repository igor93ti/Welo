using System.Collections.Generic;

namespace Welo.Domain.Interfaces.Services.GSheets
{
    public interface IGSheetsService
    {
        IList<IList<object>> BatchGet(string[] ranges);
    }
}