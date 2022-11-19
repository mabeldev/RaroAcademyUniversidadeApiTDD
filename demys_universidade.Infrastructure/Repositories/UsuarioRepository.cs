using demys_universidade.Domain.Entities;
using demys_universidade.Domain.Interfaces.Repositories;
using demys_universidade.Infrastructure.Contexts;

namespace demys_universidade.Infrastructure.Repositories
{
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(UniversidadeContext context) : base(context) { }
    }
}