using AutoMapper;
using Welo.Application.Models;
using Welo.Domain.Entities;
using Welo.Domain.Entities.Enums;

namespace Welo.Application.Maps
{
    public class ModelToDomainMappingProfile : Profile
    {
        public ModelToDomainMappingProfile()
        {
            CreateMap<StandardCommandModel, StandardCommandEntity>();
            CreateMap<LeadModel, LeadEntity>();
            CreateMap<ResponseTriggerModel, ResponseTrigger>();
            CreateMap<InfoCommandMaskModel, InfoCommandMask>();
            CreateMap<TypeButtonModel, TypeButton>();

        }
    }
}