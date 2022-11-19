using AutoMapper;
using demys_universidade.Domain.Contracts.Response;
using demys_universidade.Domain.Entities;
using demys_universidade.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace demys_universidade.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ProducesResponseType(typeof(InformacaoResponse), 400)]
    [ProducesResponseType(typeof(InformacaoResponse), 401)]
    [ProducesResponseType(typeof(InformacaoResponse), 403)]
    [ProducesResponseType(typeof(InformacaoResponse), 404)]
    [ProducesResponseType(typeof(InformacaoResponse), 500)]
    public class BaseController<TEntity, KRequest, YResponse> : ControllerBase where TEntity : BaseEntity
    {
        private readonly IMapper _mapper;
        private readonly IBaseService<TEntity> _service;

        public BaseController(IMapper mapper, IBaseService<TEntity> service)
        {
            _mapper = mapper;
            _service = service;
        }

        [HttpPost]
        [ProducesResponseType(201)]
        public virtual async Task<ActionResult> PostAsync([FromBody] KRequest request)
        {
            var entity = _mapper.Map<TEntity>(request);
            await _service.AdicionarAsync(entity);
            return Created(nameof(PostAsync), new { id = entity.Id });
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        public virtual async Task<ActionResult> PutAsync([FromRoute] int id, [FromBody] KRequest request)
        {
            var entity = _mapper.Map<TEntity>(request);
            entity.Id = id;
            await _service.AlterarAsync(entity);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        public virtual async Task<ActionResult> DeleteAsync([FromRoute] int id)
        {
            await _service.DeletarAsync(id);
            return NoContent();
        }

        [HttpGet()]
        [ProducesResponseType(200)]
        public virtual async Task<ActionResult<List<YResponse>>> GetAsync()
        {
            var entities = await _service.ObterTodosAsync();
            var response = _mapper.Map<List<YResponse>>(entities);
            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        public virtual async Task<ActionResult<YResponse>> GetByIdAsync([FromRoute] int id)
        {
            var entity = await _service.ObterPorIdAsync(id);
            var response = _mapper.Map<YResponse>(entity);
            return Ok(response);
        }
    }
}
