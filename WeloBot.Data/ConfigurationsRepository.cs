using WeloBot.Data.Repository.LiteDB;
using WeloBot.Domain.Entities;

namespace WeloBot.Data
{
    public class ConfigurationsRepository : LiteDBRepository<ConfigurationEntity, int>
    {
        public ConfigurationsRepository()
        {
            base.DbContext = new ConfigurationsContext();
        }
    }
}