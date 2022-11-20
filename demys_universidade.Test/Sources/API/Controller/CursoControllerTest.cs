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
    [Trait("Controller", "Curso Controller")]
    public class CursoControllerTest
    {
        private readonly Mock<ICursoService> _mockCursoService;
        private readonly IMapper _mapper;
        private readonly Fixture _fixture;
        public CursoControllerTest()
        {
            _mockCursoService = new Mock<ICursoService>();
            _mapper = MapConfig.Get();
            _fixture = FixtureConfig.Get();
        }

        [Fact(DisplayName = "Busca um Departamento por Id")]
        public async Task GetById()
        {
            var entity = _fixture.Create<Curso>();

            _mockCursoService.Setup(mock => mock.ObterPorIdAsync(It.IsAny<int>())).ReturnsAsync(entity);

            var controller = new CursoController(_mapper, _mockCursoService.Object);

            var response = await controller.GetByIdAsync(entity.Id);

            var objectResult = Assert.IsType<OkObjectResult>(response.Result);
            var cursoResponse = Assert.IsType<CursoResponse>(objectResult.Value);
            Assert.Equal(cursoResponse.Id, entity.Id);
        }

        [Fact(DisplayName = "Busca por Nome")]
        public async Task GetByRua()
        {
            var entity = _fixture.Create<Curso>();
            var entityTask = _fixture.Create<Task<Curso>>();

            _mockCursoService.Setup(mock => mock.ObterAsync(It.IsAny<Expression<Func<Curso, bool>>>())).Returns(entityTask);

            var controller = new CursoController(_mapper, _mockCursoService.Object);

            var response = await controller.GetAsync(entity.Nome);

            var objectResult = Assert.IsType<OkObjectResult>(response.Result);
            var usuarioResponse = Assert.IsType<List<CursoResponse>>(objectResult.Value);
            Assert.NotNull(response);
        }

        [Fact(DisplayName = "Atualiza um nome de curso existente")]
        public async Task AlterarNomeCurso()
        {
            var id = _fixture.Create<int>();
            var request = CursoFakers.CursoNomeRequestFaker();
            var nomeRequest = _fixture.Create<string>();

            _mockCursoService.Setup(mock => mock.AtualizarNomeAsync(id, nomeRequest)).Returns(Task.CompletedTask);

            var controller = new CursoController(_mapper, _mockCursoService.Object);

            var response = await controller.PatchAsync(id, request);

            var objectResult = Assert.IsType<OkResult>(response);
            Assert.Equal(StatusCodes.Status200OK, objectResult.StatusCode);
        }

        [Fact(DisplayName = "Busca todos cursos")]
        public async Task Get()
        {
            var entities = _fixture.Create<List<Curso>>();

            _mockCursoService.Setup(mock => mock.ObterTodosAsync()).ReturnsAsync(entities);

            var controller = new CursoController(_mapper, _mockCursoService.Object);

            var response = await controller.GetAsync();

            var objectResult = Assert.IsType<OkObjectResult>(response.Result);
            var cursoResponse = Assert.IsType<List<CursoResponse>>(objectResult.Value);
            Assert.True(cursoResponse.Count() > 0);
        }

        [Fact(DisplayName = "Cadastra uma novo curso")]
        public async Task Post()
        {
            var request = Fakers.CursoFakers.CursoRequestFaker();

            _mockCursoService.Setup(mock => mock.AdicionarAsync(It.IsAny<Curso>())).Returns(Task.CompletedTask);

            var controller = new CursoController(_mapper, _mockCursoService.Object);

            var response = await controller.PostAsync(request);

            var objectResult = Assert.IsType<CreatedResult>(response);
            Assert.Equal(StatusCodes.Status201Created, objectResult.StatusCode);
        }

        [Fact(DisplayName = "Atualiza uma curso existente")]
        public async Task Put()
        {
            var id = _fixture.Create<int>();
            var request = CursoFakers.CursoRequestFaker();

            _mockCursoService.Setup(mock => mock.AlterarAsync(It.IsAny<Curso>())).Returns(Task.CompletedTask);

            var controller = new CursoController(_mapper, _mockCursoService.Object);

            var response = await controller.PutAsync(id, request);

            var objectResult = Assert.IsType<NoContentResult>(response);
            Assert.Equal(StatusCodes.Status204NoContent, objectResult.StatusCode);
        }

        [Fact(DisplayName = "Remove uma curso existente")]
        public async Task Delete()
        {
            var id = _fixture.Create<int>();

            _mockCursoService.Setup(mock => mock.DeletarAsync(It.IsAny<int>())).Returns(Task.CompletedTask);

            var controller = new CursoController(_mapper, _mockCursoService.Object);

            var response = await controller.DeleteAsync(id);

            var objectResult = Assert.IsType<NoContentResult>(response);
            Assert.Equal(StatusCodes.Status204NoContent, objectResult.StatusCode);
        }
    }
}
