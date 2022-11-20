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
using System.Collections.Generic;
using System.Linq.Expressions;

namespace demys_universidade.Test.Sources.API.Controller
{
    [Trait("Controller", "Usuario Controller")]
    public class UsuarioControllerTest
    {
        private readonly Mock<IUsuarioService> _mockUsuarioService;
        private readonly IMapper _mapper;
        private readonly Fixture _fixture;
        public UsuarioControllerTest()
        {
            _mockUsuarioService = new Mock<IUsuarioService>();
            _mapper = MapConfig.Get();
            _fixture = FixtureConfig.Get();
        }

        [Fact(DisplayName = "Busca um usuário por Id")]
        public async Task GetById()
        {
            var entity = _fixture.Create<Usuario>();

            _mockUsuarioService.Setup(mock => mock.ObterPorIdUsuarioAsync(It.IsAny<int>())).ReturnsAsync(entity);

            var controller = new UsuarioController(_mapper, _mockUsuarioService.Object);

            var response = await controller.GetByIdAsync(entity.Id);

            var objectResult = Assert.IsType<OkObjectResult>(response.Result);
            var usuarioResponse = Assert.IsType<UsuarioResponse>(objectResult.Value);
            Assert.Equal(usuarioResponse.Id, entity.Id);
        }

        [Fact(DisplayName = "Busca por Nome")]
        public async Task GetByRua()
        {
            var entity = _fixture.Create<Usuario>();
            var entityTask = _fixture.Create<Task<Usuario>>();

            _mockUsuarioService.Setup(mock => mock.ObterAsync(It.IsAny<Expression<Func<Usuario, bool>>>())).Returns(entityTask);

            var controller = new UsuarioController(_mapper, _mockUsuarioService.Object);

            var response = await controller.GetAsync(entity.Nome);

            var objectResult = Assert.IsType<OkObjectResult>(response.Result);
            var usuarioResponse = Assert.IsType<List<UsuarioResponse>>(objectResult.Value);
            Assert.NotNull(response);
        }

        [Fact(DisplayName = "Busca todos usuários")]
        public async Task Get()
        {
            var entities = _fixture.Create<List<Usuario>>();

            _mockUsuarioService.Setup(mock => mock.ObterTodosAsync()).ReturnsAsync(entities);

            var controller = new UsuarioController(_mapper, _mockUsuarioService.Object);

            var response = await controller.GetAsync();

            var objectResult = Assert.IsType<OkObjectResult>(response.Result);
            var usuarioResponse = Assert.IsType<List<UsuarioResponse>>(objectResult.Value);
            Assert.True(usuarioResponse.Count() > 0);
        }

        [Fact(DisplayName = "Cadastra uma novo usuário")]
        public async Task Post()
        {
            var request = UsuarioFakers.UsuarioRequestFaker();

            _mockUsuarioService.Setup(mock => mock.AdicionarAsync(It.IsAny<Usuario>())).Returns(Task.CompletedTask);

            var controller = new UsuarioController(_mapper, _mockUsuarioService.Object);

            var response = await controller.PostAsync(request);

            var objectResult = Assert.IsType<CreatedResult>(response);
            Assert.Equal(StatusCodes.Status201Created, objectResult.StatusCode);
        }

        [Fact(DisplayName = "Atualiza uma usuário existente")]
        public async Task Put()
        {
            var id = _fixture.Create<int>();
            var request = Fakers.UsuarioFakers.UsuarioRequestFaker();

            _mockUsuarioService.Setup(mock => mock.AlterarAsync(It.IsAny<Usuario>())).Returns(Task.CompletedTask);

            var controller = new UsuarioController(_mapper, _mockUsuarioService.Object);

            var response = await controller.PutAsync(id, request);

            var objectResult = Assert.IsType<NoContentResult>(response);
            Assert.Equal(StatusCodes.Status204NoContent, objectResult.StatusCode);
        }

        [Fact(DisplayName = "Remove uma usuário existente")]
        public async Task Delete()
        {
            var id = _fixture.Create<int>();

            _mockUsuarioService.Setup(mock => mock.DeletarAsync(It.IsAny<int>())).Returns(Task.CompletedTask);

            var controller = new UsuarioController(_mapper, _mockUsuarioService.Object);

            var response = await controller.DeleteAsync(id);

            var objectResult = Assert.IsType<NoContentResult>(response);
            Assert.Equal(StatusCodes.Status204NoContent, objectResult.StatusCode);
        }
    }
}
