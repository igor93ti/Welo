using Welo.Data.Repository.LiteDB;
using Welo.Domain.Entities;

namespace Welo.Data
{
    public class ConfigurationsRepository : LiteDBRepository<ConfigurationEntity, int>
    {
        public ConfigurationsRepository()
        {
            base.DbContext = new ConfigurationsContext();
        }
    }
}