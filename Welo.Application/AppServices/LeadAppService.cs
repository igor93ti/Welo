

using System;
using Welo.Application.Interfaces;
using Welo.Data;
using Welo.Domain.Entities;
using Welo.Domain.Interfaces.Services;
using Welo.Domain.Services;
using Welo.Domain.Services.GSheets;
using Welo.GoogleDocsData;

namespace Welo.Application.AppServices
{
    [Serializable]
    public class LeadAppService : AppServiceBase<LeadEntity, int>, ILeadAppService
    {
        private readonly ILeadService _service;

        private static LeadAppService _instance;

        public static LeadAppService Intance
        {
            get
            {
                if (_instance == null)
                    lock (typeof(LeadAppService))
                        if (_instance == null)
                            _instance = new LeadAppService();

                return _instance;
            }
        }

        private LeadAppService() : base()
        {
            _service = new LeadService(new LeadRepository());
            Service = _service;
        }
        
        public void SaveSubscriber(LeadEntity lead) 
            => _service.SaveSubscriber(lead);
    }
}