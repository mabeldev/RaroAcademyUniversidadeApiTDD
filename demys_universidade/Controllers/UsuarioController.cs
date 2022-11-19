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
    public class UsuarioController : BaseController<Usuario, UsuarioRequest, UsuarioResponse>
    {
        #region Constructor

        private readonly IMapper _mapper;
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IMapper mapper, IUsuarioService service) : base (mapper, service)
        {
            _mapper = mapper;
            _usuarioService = service;
        }

        #endregion

        #region Post (Allows Anonymous)
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(201)]
        public override async Task<ActionResult> PostAsync([FromBody] UsuarioRequest request)
        {
            var entity = _mapper.Map<Usuario>(request);
            await _usuarioService.CriarUsuarioAsync(entity);
            return Created(nameof(PostAsync), new { id = entity.Id });
        }
        #endregion

        #region Get
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        public override async Task<ActionResult<UsuarioResponse>> GetByIdAsync([FromRoute] int id)
        {
            var entity = await _usuarioService.ObterPorIdUsuarioAsync(id);
            return Ok(_mapper.Map<UsuarioResponse>(entity));
        }

        #endregion

        #region Obter Por Nome
        [HttpGet("nome")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<List<UsuarioResponse>>> GetAsync([FromQuery] string nome)
        {
            var entities = await _usuarioService.ObterTodosAsync(x => x.Nome.Equals(nome));
            var response = _mapper.Map<List<UsuarioResponse>>(entities);
            return Ok(response);
        }

        #endregion

        #region Alterar Data De Nascimento
        [HttpPatch("{id}")]
        [ProducesResponseType(200)]
        public async Task<ActionResult> PatchAsync([FromRoute] int id, [FromBody] UsuarioDataNascimentoRequest request)
        {
            await _usuarioService.AtualizarDataNascimentoAsync(id, request.DataNascimento);
            return Ok();
        }

        #endregion

        #region Put
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        public override async Task<ActionResult> PutAsync([FromRoute] int id, [FromBody] UsuarioRequest request)
        {
            var entity = _mapper.Map<Usuario>(request);
            entity.Id = id;
            await _usuarioService.AtualizarUsuarioAsync(entity);
            return NoContent();
        }

        #endregion

    }

}

