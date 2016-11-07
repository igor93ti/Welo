namespace Welo.Bot.Maps
{
    using AutoMapper;
    using Domain.Entities;
    using Microsoft.Bot.Connector;
    using ViewModels;

    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<StandardCommandEntity, StandardCommandViewModel>();
            CreateMap<LeadEntity, LeadViewModel>();
            CreateMap<CardAction, TypeButton>();
            CreateMap<TypeButton, CardAction>();
            CreateMap<TypeButton, string>().ConvertUsing(src => src.ToString());
            CreateMap<TypeButton, string>().ConvertUsing(src => src.ToString());

            CreateMap<TypeButton, string>().ConvertUsing(value =>
            {
                switch (value)
                {
                    case TypeButton.ImBack:
                        return "imBack";
                    case TypeButton.PostBack:
                        return "postBack";
                    case TypeButton.Call:
                        return "call";
                    case TypeButton.PlayAudio:
                        return "playAudio";
                    case TypeButton.PlayVideo:
                        return "playVideo";
                    case TypeButton.ShowImage:
                        return "showImage";
                    case TypeButton.DownloadFile:
                        return "downloadFile";
                    case TypeButton.Signin:
                        return "signin";
                    default:
                        return "postBAck";
                }
            });
        }
    }
}