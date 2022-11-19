using demys_universidade.Domain.Entities;
using demys_universidade.Domain.Interfaces.Repositories;
using demys_universidade.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;

namespace demys_universidade.Domain.Services
{
    public class PerfilService : BaseService<Perfil>, IPerfilService
    {
        public PerfilService(IPerfilRepository repository, IHttpContextAccessor httpContextAccessor) : base(repository, httpContextAccessor)
        {
        }
    }
}
