using Welo.Domain.Entities;
using Welo.Domain.Interfaces.Repositories.Base;

namespace Welo.Domain.Interfaces.Repositories
{
    public interface ILeadRepository : IRepository<LeadEntity, int>, IRepositoryAsync<LeadEntity, int>
    {
    }
}