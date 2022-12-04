using AutoMapper;
using AutoMapper.Configuration.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetModule2_1.RabbitMq
{
    internal static class Mapping
    {
        private static readonly IMapper mapper = new MapperConfiguration(config =>
        {
            config.CreateMap<EAL.ChangedItem, BAL.Item>()
                .ForMember(item => item.Image, opt => opt.MapFrom(changedItem => 
                    changedItem.ImageUrl != null ? new BAL.Image { Url = changedItem.ImageUrl } : null));
        }).CreateMapper();

        public static TDestination Map<TSource, TDestination>(TSource source) => mapper.Map<TSource, TDestination>(source);
    }
}
