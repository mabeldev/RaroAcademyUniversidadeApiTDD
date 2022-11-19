using demys_universidade.Domain.Entities;
using demys_universidade.Domain.Interfaces.Repositories;
using demys_universidade.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;

namespace demys_universidade.Domain.Services
{
    public class DepartamentoService : BaseService<Departamento>, IDepartamentoService
    {
        #region Constructor
        private readonly IDepartamentoRepository _departamentoRepository;

        public DepartamentoService(IDepartamentoRepository departamentoRepository, IHttpContextAccessor httpContextAccessor)
                                                    : base(departamentoRepository, httpContextAccessor)
        {
            _departamentoRepository = departamentoRepository;
        }

        #endregion

        #region Atualizar Nome
        public async Task AtualizarNomeAsync(int id, string nome)
        {
            var entity = await ObterPorIdAsync(id);
            entity.Nome = nome;
            entity.DataAlteracao = DateTime.Now;
            entity.UsuarioAlteracao = UserId;
            await _departamentoRepository.EditAsync(entity);
        }

        #endregion

    }
}