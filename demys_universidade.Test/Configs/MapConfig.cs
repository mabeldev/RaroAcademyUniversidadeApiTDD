using AutoMapper;

namespace demys_universidade.Test.Configs
{
    public static class MapConfig
    {
        public static IMapper Get()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new Profiles.MappingProfile());
            });

            return mockMapper.CreateMapper();
        }
    }
}
