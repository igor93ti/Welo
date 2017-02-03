using AutoMapper;

namespace Welo.Application.Maps
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings()

        {
            Mapper.Initialize(x =>
                        {
                            x.AddProfile<DomainToModelMappingProfile>();
                            x.AddProfile<ModelToDomainMappingProfile>();
                        });
        }
    }
}