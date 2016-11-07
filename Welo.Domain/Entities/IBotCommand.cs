using System.Collections.Generic;
using Welo.Domain.Entities.Base;
using Welo.Domain.Entities.Enums;

namespace Welo.Domain.Entities
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

        Option FormatMessage(IList<object> row);
        Option GetResponse(IList<object> row);
        Option GetResponseQuote();
    }
}