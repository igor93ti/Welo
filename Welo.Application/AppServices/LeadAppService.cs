

using System;
using Welo.Application.Interfaces;
using Welo.Domain.Entities;
using Welo.Domain.Interfaces.Services;

namespace Welo.Application.AppServices
{
    [Serializable]
    public class LeadAppService : AppServiceBase<LeadEntity, int>, ILeadAppService
    {
        private readonly ILeadService _service;

        public LeadAppService(ILeadService service) : base(service)
        {
            _service = service;
        }

        public void SaveSubscriber(LeadEntity lead) => _service.SaveSubscriber(lead);

    }
}