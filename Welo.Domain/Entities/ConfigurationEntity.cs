using System;
using Welo.Domain.Entities.Base;

namespace Welo.Domain.Entities
{
    [Serializable]
    public class ConfigurationEntity : Entity<int>
    {
        public DateTime DataUltimaAtualizacao { get; set; }
    }
}