using System;

namespace Welo.Application.Models
{
    [Serializable]
    public class ButtonOptionModel
    {
        public string Value { get; set; }
        public TypeButtonModel Type { get; set; }
        public string Title { get; set; }
    }
}