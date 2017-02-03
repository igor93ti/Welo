using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Welo.Domain.Entities.Base;
using Welo.Domain.Entities.Enums;
using Welo.Domain.Interfaces;

namespace Welo.Domain.Entities
{
    [Serializable]
    public class StandardCommandEntity : Entity<int> , IBotCommand
    {
        public string Trigger { get; set; }

        public bool IsRandomResponse { get; set; }

        public bool IsVisibleOnMenu { get; set; }

        public CommandType CommandType { get; set; }

        public IList<int> FormatMask { get; set; }

        public InfoCommandMask InfoMask { get; set; }

        public string TableName { get; set; }

        public string[] QuotesResponses { get; set; }

        public string Name { get; set; }

        public bool WithButtons { get; set; }

        public ResponseTrigger FormatMessage(IList<object> row)
        {
            var response = new ResponseTrigger();
            if (InfoMask != null)
            {
                response.Title = InfoMask.Title >= 0 ? row[InfoMask.Title].ToString() : string.Empty;
                response.Author = InfoMask.Author >= 0 ? row[InfoMask.Author].ToString() : string.Empty;
                response.Quote = InfoMask.Quote >= 0 ? row[InfoMask.Quote].ToString() : string.Empty;
                response.Link = InfoMask.Link >= 0 ? row[InfoMask.Link].ToString() : string.Empty;
            }

            var strBuilder = new StringBuilder();

            foreach (var index in FormatMask)
            {
                if (row[index] == null)
                    return response;

                strBuilder.Append(row[index]);

                if (index != FormatMask.Last())
                    strBuilder.Append(" - ");
            }

            response.MessageFormated = strBuilder.ToString();
            return response;
        }

        public ResponseTrigger GetResponse(IList<object> row) => FormatMessage(row);

        public ResponseTrigger GetResponseQuote()
        {
            var message = new StringBuilder();
            string quoteResponse = null;
            if (IsRandomResponse)
            {
                var rdm = new Random();
                quoteResponse = QuotesResponses[rdm.Next(QuotesResponses.Length)];
                message.Append(quoteResponse);
            }
            else
                foreach (var item in QuotesResponses)
                    message.AppendLine(item);

            var response = message.ToString();
            return new ResponseTrigger { Quote = response, MessageFormated = response };
        }
    }
}