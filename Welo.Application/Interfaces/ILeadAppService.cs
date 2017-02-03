using Welo.Domain.Entities;

namespace Welo.Application.Interfaces
{
    public interface ILeadAppService : IAppServiceBase<LeadEntity, int>
    {
        void SaveSubscriber(LeadEntity lead);
    }
}