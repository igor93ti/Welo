using WeloBot.Data.Repository.LiteDB;
using WeloBot.Domain.Entities;
using WeloBot.Domain.Interfaces.Repositories;

namespace WeloBot.Data
{
    public class StandardCommandRepository : LiteDBRepository<StandardCommandEntity, int>, IStandardCommandRepository
    {
        public StandardCommandRepository()
        {
            base.DbContext = new CommandsContext();
        }
    }
}