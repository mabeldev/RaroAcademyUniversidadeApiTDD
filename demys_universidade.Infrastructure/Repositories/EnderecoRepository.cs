using demys_universidade.Domain.Entities;
using demys_universidade.Domain.Interfaces.Repositories;
using demys_universidade.Infrastructure.Contexts;

namespace demys_universidade.Infrastructure.Repositories
{
    public class EnderecoRepository : BaseRepository<Endereco>, IEnderecoRepository
    {
        public EnderecoRepository(UniversidadeContext context) : base(context) { }

    }
}
