namespace WeloBot.Api.Maps
{
    using AutoMapper;
    using Domain.Entities;
    using ViewModels;

    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<StandardCommandViewModel, StandardCommandEntity>();
        }
    }
}