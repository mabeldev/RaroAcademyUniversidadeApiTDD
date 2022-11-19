using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace demys_universidade.Test.Configs
{
    public static class ClaimConfig
    {
        public static IEnumerable<Claim> Get(int id, string nome, string cpf, string perfil)
        {
            return new Claim[]
                {
                new Claim(ClaimTypes.NameIdentifier, id.ToString()),
                new Claim(ClaimTypes.Name, nome),
                new Claim(ClaimTypes.Email, cpf),
                new Claim(ClaimTypes.Role, perfil)
                };
        }
    }
}
