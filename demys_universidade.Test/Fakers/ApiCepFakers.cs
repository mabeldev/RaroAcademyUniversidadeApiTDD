using Bogus;
using demys_universidade.Domain.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demys_universidade.Test.Fakers
{
    public static class ApiCepFakers
    {
        private static readonly Faker Fake = new Faker();

        public static AppSetting CepOptions() => new AppSetting
        {
            ApiCep = Fake.Internet.Url()
        };

    }
}
