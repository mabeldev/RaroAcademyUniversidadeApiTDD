using AutoMapper;
using demys_universidade.Domain.Contracts.Request;
using demys_universidade.Domain.Contracts.Response;
using demys_universidade.Domain.Entities;
using demys_universidade.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace demys_universidade.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BrasilCepController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IEnderecoService _enderecoService;

        public BrasilCepController(IMapper mapper, IEnderecoService enderecoService)
        {
            _mapper = mapper;
            _enderecoService = enderecoService;
        }

        [HttpGet("rua/{rua}")]
        [ProducesResponseType(200)]
        public async Task<ActionResult> GetPorRua([FromRoute] string rua)
        {
            var entity = await _enderecoService.GetPorRua(rua);
            var endereco = _mapper.Map<EnderecoResponse>(entity);
            return Ok(endereco);
        }

        [HttpGet("cep/{cep}")]
        [ProducesResponseType(200)]
        public async Task<ActionResult> GetPorCep([FromRoute] string cep)
        {
            var entity = await _enderecoService.GetPorCep(cep);
            var endereco = _mapper.Map<EnderecoResponse>(entity);
            return Ok(endereco);
        }
    }

}

