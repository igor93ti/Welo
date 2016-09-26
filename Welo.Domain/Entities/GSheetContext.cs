using System;
using System.Collections.Generic;
using System.Data.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Welo.Domain.Interfaces.Services.GSheets;

namespace Welo.Domain.Entities
{
    public class GSheetContext
    {
        public string ApplicationName { get; set; }
        public string SpreadsheetId { get; set; }
        public string PathConfig { get; set; }
        public string NameFile { get; set; }
        public string User { get; set; }
    }
}
