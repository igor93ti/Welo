using System.Collections.Generic;
using Welo.Domain.Entities.Base;
using Welo.Domain.Entities.Enums;

namespace Welo.Domain.Entities
{
    public interface IBotCommand : IEntity<int>
    {
        string Trigger { get; set; }
        CommandType CommandType { get; set; }
        string TableName { get; set; }
        ResponseTrigger GetMessageWithRandomQuote(IList<object> row);
    }
}