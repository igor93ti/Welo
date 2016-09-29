using System.Collections.Generic;
using Welo.Domain.Entities;

namespace Welo.Domain.Services.GSheets
{
    public interface ICommandTextGoogle
    {
        IList<object> GetRandomRowGSheets(GSheetQuery range);
    }
}