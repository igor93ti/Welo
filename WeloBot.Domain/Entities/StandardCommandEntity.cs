using WeloBot.Domain.Entities.Base;
using WeloBot.Domain.Entities.Enums;

namespace WeloBot.Domain.Entities
{
    public class StandardCommandEntity : Entity<int>
    {
        public string Trigger { get; set; }
        public string ResponseMessages { get; set; }
        public bool IsRandomResponse { get; set; }
        public bool IsVisibleOnMenu { get; set; }
        public CommandType CommandType { get; set; }
    }
}