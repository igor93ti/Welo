using System;
using Welo.Domain.Entities;
using Welo.Domain.Interfaces.Repositories;
using Welo.Domain.Interfaces.Services;

namespace Welo.Domain.Services
{
    [Serializable]
    public class StandartCommandsService : ServiceBaseTEntity<StandardCommandEntity, int>, IStandardCommandService
    {
        private readonly IStandardCommandRepository _standardCommandRepository;

        public StandartCommandsService(IStandardCommandRepository standardCommandRepository) : base(standardCommandRepository)
        {
            _standardCommandRepository = standardCommandRepository;
        }
    }
}