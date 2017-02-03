using System.Collections.Generic;
using Welo.Domain.Entities;
using Welo.Domain.Entities.Base;
using Welo.Domain.Entities.Enums;

namespace Welo.Domain.Interfaces
{
    public interface IBotCommand : IEntity<int>
    {
        CommandType CommandType { get; set; }
        IList<int> FormatMask { get; set; }
        InfoCommandMask InfoMask { get; set; }
        bool IsRandomResponse { get; set; }
        bool IsVisibleOnMenu { get; set; }
        string Name { get; set; }
        string[] QuotesResponses { get; set; }
        string TableName { get; set; }
        string Trigger { get; set; }
        bool WithButtons { get; set; }

        ResponseTrigger FormatMessage(IList<object> row);
        ResponseTrigger GetResponse(IList<object> row);
        ResponseTrigger GetResponseQuote();
    }
}