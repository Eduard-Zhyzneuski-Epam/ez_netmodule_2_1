using AutoMapper;

namespace NetModule2_1.DefaultBusinessLogic
{
    internal static class Mapping
    {
        private static readonly IMapper mapper = new MapperConfiguration(c =>
        {
            c.CreateMap<BAL.Item, DAL.Item>();
            c.CreateMap<BAL.Image, DAL.Image>();
            c.CreateMap<DAL.Item, BAL.Item>();
            c.CreateMap<DAL.Image, BAL.Image>();
        }).CreateMapper();

        internal static TDest Map<TSource, TDest>(TSource sourceObject) => mapper.Map<TSource, TDest>(sourceObject);
    }
}
