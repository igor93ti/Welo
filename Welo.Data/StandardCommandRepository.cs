using System;
using Welo.Data.Repository.LiteDB;
using Welo.Domain.Entities;
using Welo.Domain.Interfaces.Repositories;

namespace Welo.Data
{
    [Serializable]
    public class StandardCommandRepository : LiteDBRepository<StandardCommandEntity, int>, IStandardCommandRepository
    {
        public StandardCommandRepository()
        {
            base.DbContext = new CommandsContext();
        }
    }
}