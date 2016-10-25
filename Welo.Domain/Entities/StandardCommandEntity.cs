using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Welo.Domain.Entities.Base;
using Welo.Domain.Entities.Enums;

namespace Welo.Domain.Entities
{
    [Serializable]
    public class StandardCommandEntity : Entity<int>
    {
        public string Trigger { get; set; }
        public bool IsRandomResponse { get; set; }
        public bool IsVisibleOnMenu { get; set; }
        public CommandType CommandType { get; set; }
        public IList<int> FormatMask { get; set; }
        public string TableName { get; set; }
        public string[] QuotesResponses { get; set; }

        public string GetMessage(IList<object> row)
        {
            var strBuilder = new StringBuilder();

            foreach (var index in FormatMask)
            {
                if (row[index] == null)
                    return strBuilder.ToString();

                strBuilder.Append(row[index]);

                if (index != FormatMask.Last())
                    strBuilder.Append(" - ");
            }

            return strBuilder.ToString();
        }

        public string GetMessageWithRandomQuote(IList<object> row)
        {
            var message = new StringBuilder();
            var rdm = new Random();
            var quoteResponse = QuotesResponses[rdm.Next(QuotesResponses.Length)];
            message.Append(quoteResponse);
            message.AppendLine(GetMessage(row));
            return message.ToString();
        }
    }
}