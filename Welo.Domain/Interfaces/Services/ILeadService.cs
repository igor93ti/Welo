using Welo.Domain.Entities;

namespace Welo.Domain.Interfaces.Services
{
    public interface ILeadService : IService<LeadEntity, int>
    {
        void SaveSubscriber(LeadEntity lead);
    }
}