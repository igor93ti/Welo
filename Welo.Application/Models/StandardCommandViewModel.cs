using System;
using System.Collections.Generic;
using Welo.Domain.Entities;

namespace Welo.Application.Models
{
    [Serializable]
    public class StandardCommandModel
    {
        public string Trigger { get; set; }
        public bool IsRandomResponse { get; set; }
        public bool IsVisibleOnMenu { get; set; }
        public CommandTypeViewModel CommandType { get; set; }
        public IList<int> FormatMask { get; set; }
        public string TableName { get; set; }
        public string[] QuotesResponses { get; set; }
        public InfoCommandMaskModel InfoMask { get; set; }

        public string Name { get; set; }

        public bool WithButtons { get; set; }
        public int Id { get; set; }
    }
}