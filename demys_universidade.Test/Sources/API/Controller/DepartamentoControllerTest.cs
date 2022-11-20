using AutoFixture;
using AutoMapper;
using demys_universidade.Controllers;
using demys_universidade.Domain.Contracts.Response;
using demys_universidade.Domain.Entities;
using demys_universidade.Domain.Interfaces.Services;
using demys_universidade.Test.Configs;
using demys_universidade.Test.Fakers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Linq.Expressions;

namespace demys_universidade.Test.Sources.API.Controller
{
    [Trait("Controller", "Departamento Controller")]
    public class DepartamentoControllerTest
    {
        private readonly Mock<IDepartamentoService> _mockDepartamentoService;
        private readonly IMapper _mapper;
        private readonly Fixture _fixture;
        public DepartamentoControllerTest()
        {
            _mockDepartamentoService = new Mock<IDepartamentoService>();
            _mapper = MapConfig.Get();
            _fixture = FixtureConfig.Get();
        }

        [Fact(DisplayName = "Busca um Departamento por Id")]
        public async Task GetById()
        {
            var entity = _fixture.Create<Departamento>();

            _mockDepartamentoService.Setup(mock => mock.ObterPorIdAsync(It.IsAny<int>())).ReturnsAsync(entity);

            var controller = new DepartamentoController(_mapper, _mockDepartamentoService.Object);

            var response = await controller.GetByIdAsync(entity.Id);

            var objectResult = Assert.IsType<OkObjectResult>(response.Result);
            var departamentoResponse = Assert.IsType<DepartamentoResponse>(objectResult.Value);
            Assert.Equal(departamentoResponse.Id, entity.Id);
        }

        [Fact(DisplayName = "Busca todos departamentos")]
        public async Task Get()
        {
            var entities = _fixture.Create<List<Departamento>>();

            _mockDepartamentoService.Setup(mock => mock.ObterTodosAsync()).ReturnsAsync(entities);

            var controller = new DepartamentoController(_mapper, _mockDepartamentoService.Object);

            var response = await controller.GetAsync();

            var objectResult = Assert.IsType<OkObjectResult>(response.Result);
            var empresasResponse = Assert.IsType<List<DepartamentoResponse>>(objectResult.Value);
            Assert.True(empresasResponse.Count() > 0);
        }

        [Fact(DisplayName = "Busca por Nome")]
        public async Task GetByNome()
        {
            var entity = _fixture.Create<Departamento>();
            var entityTask = _fixture.Create<Task<Departamento>>();

            _mockDepartamentoService.Setup(mock => mock.ObterAsync(It.IsAny<Expression<Func<Departamento, bool>>>())).Returns(entityTask);

            var controller = new DepartamentoController(_mapper, _mockDepartamentoService.Object);

            var response = await controller.GetAsync(entity.Nome);

            var objectResult = Assert.IsType<OkObjectResult>(response.Result);
            var usuarioResponse = Assert.IsType<List<DepartamentoResponse>>(objectResult.Value);
            Assert.NotNull(response);
        }

        [Fact(DisplayName = "Cadastra um novo departamento")]
        public async Task Post()
        {
            var request = Fakers.DepartamentoFakers.DepartamentoRequestFaker();

            _mockDepartamentoService.Setup(mock => mock.AdicionarAsync(It.IsAny<Departamento>())).Returns(Task.CompletedTask);

            var controller = new DepartamentoController(_mapper, _mockDepartamentoService.Object);

            var response = await controller.PostAsync(request);

            var objectResult = Assert.IsType<CreatedResult>(response);
            Assert.Equal(StatusCodes.Status201Created, objectResult.StatusCode);
        }

        [Fact(DisplayName = "Atualiza um departamento existente")]
        public async Task Put()
        {
            var id = _fixture.Create<int>();
            var request = Fakers.DepartamentoFakers.DepartamentoRequestFaker();

            _mockDepartamentoService.Setup(mock => mock.AlterarAsync(It.IsAny<Departamento>())).Returns(Task.CompletedTask);

            var controller = new DepartamentoController(_mapper, _mockDepartamentoService.Object);

            var response = await controller.PutAsync(id, request);

            var objectResult = Assert.IsType<NoContentResult>(response);
            Assert.Equal(StatusCodes.Status204NoContent, objectResult.StatusCode);
        }

        [Fact(DisplayName = "Atualiza um nome de departamento existente")]
        public async Task AlterarNomeDpartamento()
        {
            var id = _fixture.Create<int>();
            var request = DepartamentoFakers.DepartamentoNomeRequestFaker();
            var nomeRequest = _fixture.Create<string>();

            _mockDepartamentoService.Setup(mock => mock.AtualizarNomeAsync(id, nomeRequest)).Returns(Task.CompletedTask);

            var controller = new DepartamentoController(_mapper, _mockDepartamentoService.Object);

            var response = await controller.PatchAsync(id, request);

            var objectResult = Assert.IsType<OkResult>(response);
            Assert.Equal(StatusCodes.Status200OK, objectResult.StatusCode);
        }

        [Fact(DisplayName = "Remove um departamento existente")]
        public async Task Delete()
        {
            var id = _fixture.Create<int>();

            _mockDepartamentoService.Setup(mock => mock.DeletarAsync(It.IsAny<int>())).Returns(Task.CompletedTask);

            var controller = new DepartamentoController(_mapper, _mockDepartamentoService.Object);

            var response = await controller.DeleteAsync(id);

            var objectResult = Assert.IsType<NoContentResult>(response);
            Assert.Equal(StatusCodes.Status204NoContent, objectResult.StatusCode);
        }
    }
}
