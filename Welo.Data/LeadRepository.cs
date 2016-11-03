using System;
using Welo.Data.Repository.LiteDB;
using Welo.Domain.Entities;
using Welo.Domain.Interfaces.Repositories;

namespace Welo.Data
{
    [Serializable]
    public class LeadRepository : LiteDBRepository<LeadEntity, int>, ILeadRepository
    {
        public LeadRepository()
        {
            base.DbContext = new CommandsContext();
        }

        public override LeadEntity Add(LeadEntity entity)
        {
            entity.LastUpdate = DateTime.Now;
            return base.Add(entity);
        }

        public override LeadEntity Update(LeadEntity entity)
        {
            entity.LastUpdate = DateTime.Now;
            return base.Update(entity);
        }
    }
}