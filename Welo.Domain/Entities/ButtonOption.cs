using System;
using Welo.Domain.Entities.Enums;

namespace Welo.Domain.Entities
{
    [Serializable]
    public class ButtonOption
    {
        public string Value { get; set; }
        public TypeButton Type { get; set; }
        public string Title { get; set; }
    }
}