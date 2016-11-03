using System;
using System.Collections.Generic;
using System.Linq;
using Welo.Domain.Entities;
using Welo.Domain.Entities.Enums;
using Welo.Domain.Interfaces.Repositories;
using Welo.Domain.Interfaces.Services;
using Welo.Domain.Services.GSheets;

namespace Welo.Domain.Services
{
    [Serializable]
    public class LeadService : ServiceBaseTEntity<LeadEntity, int>, ILeadService
    {
        private readonly ILeadRepository _leadRepository;
     
        public LeadService(ILeadRepository leadRepository)
            : base(leadRepository)
        {
            _leadRepository = leadRepository;
        }

        public void SaveSubscriber(LeadEntity leadEntity)
        {
            var lead =_leadRepository.Find(x => x.IdUser == leadEntity.IdUser).SingleOrDefault();

            if (lead == null)
                _leadRepository.Add(leadEntity);
            else
            {
                leadEntity.Id = lead.Id;
                _leadRepository.Update(leadEntity);
            }
        }
    }
}