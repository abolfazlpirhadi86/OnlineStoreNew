namespace Common.ServiceHelpers
{
    public interface IMapperService
    {
        TDestination Map<TDestination>(object source);
        IQueryable<TDestination> ProjectTo<TDestination>(IQueryable source);
        TDestination Map<TSource, TDestination>(TSource source, TDestination destination);
    }
}
