using AutoMapper;

namespace OnlineStore.Common.Services.Mapper.Configurations
{
    public class CustomMappingProfile : Profile
    {
        public CustomMappingProfile(IEnumerable<ICutomMapping> haveCustomMappings)
        {
            foreach (var item in haveCustomMappings)
                item.CreateMappings(this);
        }
    }
}
