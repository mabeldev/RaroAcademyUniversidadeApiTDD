using demys_universidade.Domain.Contracts.Request;
using demys_universidade.Domain.Contracts.Response;
using demys_universidade.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace demys_universidade.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(InformacaoResponse), 400)]
    [ProducesResponseType(typeof(InformacaoResponse), 401)]
    [ProducesResponseType(typeof(InformacaoResponse), 403)]
    [ProducesResponseType(typeof(InformacaoResponse), 404)]
    [ProducesResponseType(typeof(InformacaoResponse), 500)]
    public class AutenticacoesController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public AutenticacoesController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost]
        [ProducesResponseType(200)]
        public async Task<ActionResult<AutenticacaoResponse>> PostAsync([FromBody] AutenticacaoRequest request)
        {
            var response = await _usuarioService.AutenticarAsync(request.CPF, request.Senha);
            return Ok(response);
        }
    }

}
