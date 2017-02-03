using AutoMapper;
using Welo.Application.Models;
using Welo.Domain.Entities;
using Welo.Domain.Entities.Enums;

namespace Welo.Application.Maps
{
    public class DomainToModelMappingProfile : Profile
    {
        public DomainToModelMappingProfile()
        {
            CreateMap<StandardCommandEntity, StandardCommandModel>();
            CreateMap<LeadEntity, LeadModel>();
            CreateMap<ResponseTrigger, ResponseTriggerModel>();
            CreateMap<InfoCommandMask, InfoCommandMaskModel>();

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