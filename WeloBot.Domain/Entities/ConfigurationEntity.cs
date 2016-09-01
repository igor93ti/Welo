using System;
using WeloBot.Domain.Entities.Base;

namespace WeloBot.Domain.Entities
{
    public class ConfigurationEntity : Entity<int>
    {
        public DateTime DataUltimaAtualizacao { get; set; }
    }
}