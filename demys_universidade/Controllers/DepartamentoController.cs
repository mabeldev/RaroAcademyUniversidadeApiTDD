using AutoMapper;
using demys_universidade.Domain.Contracts.Request;
using demys_universidade.Domain.Contracts.Response;
using demys_universidade.Domain.Entities;
using demys_universidade.Domain.Interfaces.Services;
using demys_universidade.Domain.Services;
using demys_universidade.Domain.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace demys_universidade.Controllers
{
    [Authorize(Roles = ConstanteUtil.PerfilLogadoNome)]

    public class DepartamentoController
        : BaseController<Departamento, DepartamentoRequest, DepartamentoResponse>
    {

        #region Constructor
        private readonly IMapper _mapper;
        private readonly IDepartamentoService _departamentoService;

        public DepartamentoController(IMapper mapper, IDepartamentoService service)
            : base(mapper, service)
        {
            _mapper = mapper;
            _departamentoService = service;
        }

        #endregion

        #region Post (Allow Anonymous)
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(201)]
        public override async Task<ActionResult> PostAsync([FromBody] DepartamentoRequest request)
        {
            var entity = _mapper.Map<Departamento>(request);
            await _departamentoService.AdicionarAsync(entity);
            return Created(nameof(PostAsync), new { id = entity.Id });
        }

        #endregion

        #region Obter Por Nome
        [HttpGet("nome")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<List<DepartamentoResponse>>> GetAsync([FromQuery] string nome)
        {
            var entities = await _departamentoService.ObterTodosAsync(x => x.Nome.Equals(nome));
            var response = _mapper.Map<List<DepartamentoResponse>>(entities);
            return Ok(response);
        }

        #endregion

        #region Alterar Nome
        [HttpPatch("{id}")]
        [ProducesResponseType(200)]
        public async Task<ActionResult> PatchAsync([FromRoute] int id, [FromBody] DepartamentoNomeRequest request)
        {
            await _departamentoService.AtualizarNomeAsync(id, request.Nome);
            return Ok();
        }

        #endregion

    }
}

