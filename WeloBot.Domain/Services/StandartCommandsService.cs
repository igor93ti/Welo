using WeloBot.Domain.Entities;
using WeloBot.Domain.Interfaces.Repositories;
using WeloBot.Domain.Interfaces.Services;

namespace WeloBot.Domain.Services
{
    public class StandartCommandsService : ServiceBaseTEntity<StandardCommandEntity, int>, IStandardCommandService
    {
        private readonly IStandardCommandRepository _standardCommandRepository;

        public StandartCommandsService(IStandardCommandRepository standardCommandRepository) : base(standardCommandRepository)
        {
            _standardCommandRepository = standardCommandRepository;
        }
    }
}