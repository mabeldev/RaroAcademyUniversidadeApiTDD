using demys_universidade.Domain.Entities;
using demys_universidade.Domain.Interfaces.Repositories;
using demys_universidade.Infrastructure.Contexts;

namespace demys_universidade.Infrastructure.Repositories
{
    public class DepartamentoRepository : BaseRepository<Departamento>, IDepartamentoRepository
    {
        public DepartamentoRepository(UniversidadeContext context) : base(context) { }
        
    }
}
