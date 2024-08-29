using AutoMapper;

namespace OnlineStore.Common.Services.Mapper.AutoMapper
{
    public class MapperService : IMapperService
    {
        private readonly IMapper _mapper;
        public MapperService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public TDestination Map<TDestination>(object source)
        {
            return _mapper.Map<TDestination>(source);
        }
        public IQueryable<TDestination> ProjectTo<TDestination>(IQueryable source)
        {
            return _mapper.ProjectTo<TDestination>(source);
        }
        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            return _mapper.Map(source, destination);
        }
    }
}
