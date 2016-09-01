﻿namespace WeloBot.Api.Maps
{
    using AutoMapper;
    using Domain.Entities;
    using ViewModels;

    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<StandardCommandEntity, StandardCommandViewModel>();
        }
    }
}