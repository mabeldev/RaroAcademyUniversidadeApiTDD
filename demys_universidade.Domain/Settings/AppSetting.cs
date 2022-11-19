using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demys_universidade.Domain.Settings
{
    public class AppSetting
    {
        public string SqlServerConnection { get; set; }
        public string ApiCep { get; set; }
        public string JwtSecurityKey { get; set; }
    }
}