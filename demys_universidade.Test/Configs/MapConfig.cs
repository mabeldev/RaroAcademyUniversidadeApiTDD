using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demys_universidade.Test.Configs
{
    public static class MapConfig
    {
        public static IMapper Get()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new demys_universidade.Profiles.MappingProfile());
            });

            return mockMapper.CreateMapper();
        }
    }
}
