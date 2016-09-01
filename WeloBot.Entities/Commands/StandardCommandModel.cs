using WeloBot.Entities.Base;
using WeloBot.Entities.Enums;

namespace WeloBot.Entities.Commands
{
    public class StandardCommandModel : Entity<int>
    {
        public string Trigger { get; set; }
        public string ResponseMessages { get; set; }
        public bool IsRandomResponse { get; set; }
        public bool IsVisibleOnMenu { get; set; }
        public CommandType CommandType { get; set; }
    }
}