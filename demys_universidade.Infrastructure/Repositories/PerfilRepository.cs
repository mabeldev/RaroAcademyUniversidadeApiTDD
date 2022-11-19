using demys_universidade.Domain.Entities;
using demys_universidade.Domain.Interfaces.Repositories;
using demys_universidade.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demys_universidade.Infrastructure.Repositories
{
    public class PerfilRepository : BaseRepository<Perfil>, IPerfilRepository
    {
        public PerfilRepository(UniversidadeContext universidadeContext) : base(universidadeContext)
        {
        }
    }
}
