using demys_universidade.Domain.Entities;
using System.Security.Claims;

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
        public static Claim[] Claims(this Usuario usuario)
        {
            return new Claim[]
                {
            new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new Claim(ClaimTypes.Name, usuario.Nome),
            new Claim(ClaimTypes.Email, usuario.CPF),
            new Claim(ClaimTypes.Role, usuario.Perfil.Nome)
                };
        }
    }
}
