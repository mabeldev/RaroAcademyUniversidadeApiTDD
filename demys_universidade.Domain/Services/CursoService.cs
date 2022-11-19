using demys_universidade.Domain.Entities;
using demys_universidade.Domain.Interfaces.Repositories;
using demys_universidade.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;

namespace demys_universidade.Domain.Services
{
    public class CursoService : BaseService<Curso>, ICursoService
    {
        #region Constructor
        public readonly ICursoRepository _cursoRepository;
        public CursoService(ICursoRepository cursoRepository, IHttpContextAccessor httpContextAccessor) : base(cursoRepository, httpContextAccessor)
        {
            _cursoRepository = cursoRepository;
        }

        #endregion

        #region Atualizar Nome
        public async Task AtualizarNomeAsync(int id, string nome)
        {
            var entity = await ObterPorIdAsync(id);
            entity.Nome = nome;
            entity.DataAlteracao = DateTime.Now;
            entity.UsuarioAlteracao = UserId;
            await _cursoRepository.EditAsync(entity);
        }

        #endregion
    }
}
