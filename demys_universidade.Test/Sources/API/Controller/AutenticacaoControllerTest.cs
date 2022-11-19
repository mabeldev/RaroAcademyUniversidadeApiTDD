using AutoFixture;
using AutoMapper;
using demys_universidade.Controllers;
using demys_universidade.Domain.Contracts.Request;
using demys_universidade.Domain.Contracts.Response;
using demys_universidade.Domain.Interfaces.Services;
using demys_universidade.Test.Configs;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace demys_universidade.Test.Sources.API.Controller
{
    [Trait("Controller", "Controller Autenticação")]
    public class AutenticacoesControllerTest
    {
        private readonly Mock<IUsuarioService> _mockUsuarioService;
        private readonly IMapper _mapper;
        private readonly Fixture _fixture;

        public AutenticacoesControllerTest()
        {
            _mockUsuarioService = new Mock<IUsuarioService>();
            _mapper = MapConfig.Get();
            _fixture = FixtureConfig.Get();
        }

        [Fact(DisplayName = "Cria Token")]
        public async Task Post()
        {
            var request = _fixture.Create<AutenticacaoRequest>();
            var response = _fixture.Create<AutenticacaoResponse>();

            _mockUsuarioService.Setup(mock => mock.AutenticarAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(response);

            var controller = new AutenticacoesController(_mockUsuarioService.Object);

            var actionResult = await controller.PostAsync(request);

            var objectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var responseResult = Assert.IsType<AutenticacaoResponse>(objectResult.Value);
            Assert.Equal(responseResult.Token, response.Token);
        }
    }
}
