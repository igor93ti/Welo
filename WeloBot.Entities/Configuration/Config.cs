using System;
using WeloBot.Entities.Base;

namespace WeloBot.Entities.Configuration
{
    public class Configurations : Entity<int>
    {
        public DateTime DataUltimaAtualizacao { get; set; }
    }
}