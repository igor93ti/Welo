using System.Collections.Generic;
using Welo.Domain.Entities.GDocs;

namespace Welo.Domain.Services.GSheets
{
    public interface ICommandTextGoogle
    {
        IList<object> GetRandomRowGSheets(GSheetQuery range);
    }
}