using AutoFixture;
using AutoMapper;
using Bogus;
using demys_universidade.Controllers;
using demys_universidade.Domain.Contracts.Response;
using demys_universidade.Domain.Entities;
using demys_universidade.Domain.Interfaces.Services;
using demys_universidade.Test.Configs;
using demys_universidade.Test.Fakers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace demys_universidade.Test.Sources.API.Controller
{
    [Trait("Controller", "Endereco Controller")]
    public class BrasilCepControllerTest
    {
        private readonly Mock<IEnderecoService> _mockEnderecoService;
        private readonly IMapper _mapper;
        private readonly Fixture _fixture;

        public BrasilCepControllerTest()
        {
            _mockEnderecoService = new Mock<IEnderecoService>();
            _mapper = MapConfig.Get();
            _fixture = FixtureConfig.Get();
        }

        [Fact(DisplayName = "Busca CEP")]
        public async Task GetCep()
        {
            var entity = _fixture.Create<BrasilCep>();

            _mockEnderecoService.Setup(mock => mock.GetPorCep(It.IsAny<string>())).ReturnsAsync(entity);

            var controller = new BrasilCepController(_mapper, _mockEnderecoService.Object);

            var actionResult = await controller.GetPorCep(entity.Cep);

            var objectResult = Assert.IsType<OkObjectResult>(actionResult);
            var response = Assert.IsType<EnderecoResponse>(objectResult.Value);
            Assert.NotNull(response.CEP);
        }

        [Fact(DisplayName = "Busca por Rua")]
        public async Task GetByRua()
        {
            var entity = EnderecoFakers.EnderecoFaker();

            _mockEnderecoService.Setup(mock => mock.GetPorRua(It.IsAny<string>())).Returns(EnderecoFakers.EnderecoTask);

            var controller = new BrasilCepController(_mapper, _mockEnderecoService.Object);

            var actionResult = await controller.GetPorRua(entity.Rua);

            var objectResult = Assert.IsType<OkObjectResult>(actionResult);
            var response = Assert.IsType<EnderecoResponse>(objectResult.Value);
            Assert.NotNull(response.Rua);
        }        
    }
}
