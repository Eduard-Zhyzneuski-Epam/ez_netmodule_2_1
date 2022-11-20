using AutoMapper;

namespace NetModule2_1.API
{
    public static class Mapping
    {
        private static IMapper mapper;

        private static IMapper Mapper 
        { 
            get 
            {
                if (mapper is null)
                    mapper = InitMapper();
                return mapper; 
            } 
        }

        private static IMapper InitMapper()
        {
            return new MapperConfiguration(config =>
            {
                config.CreateMap<BAL.Item, Models.Item>();
                config.CreateMap<BAL.Image, Models.Image>();
                config.CreateMap<Models.Item, BAL.Item>();
                config.CreateMap<Models.Item, BAL.Item>();
            }).CreateMapper();
        }

        public static TDestination Map<TSource, TDestination>(TSource source)
        {
            return Mapper.Map<TSource, TDestination>(source);
        }

        public static List<TDestination> MapList<TSource, TDestination>(List<TSource> source)
        {
            return source.Select(sourceElement => Mapper.Map<TSource, TDestination>(sourceElement)).ToList();
        }
    }
}
