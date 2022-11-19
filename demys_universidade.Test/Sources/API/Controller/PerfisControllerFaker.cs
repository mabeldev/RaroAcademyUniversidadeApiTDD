﻿using AutoFixture;
using AutoMapper;
using demys_universidade.Controllers;
using demys_universidade.Domain.Contracts.Request;
using demys_universidade.Domain.Contracts.Response;
using demys_universidade.Domain.Entities;
using demys_universidade.Domain.Interfaces.Services;
using demys_universidade.Test.Configs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace demys_universidade.Test.Sources.API.Controller
{
    [Trait("Controller", "Controller Perfis")]
    public class PerfisControllerTest
    {
        private readonly Mock<IPerfilService> _mockPerfilService;
        private readonly IMapper _mapper;
        private readonly Fixture _fixture;

        public PerfisControllerTest()
        {
            _mockPerfilService = new Mock<IPerfilService>();
            _mapper = MapConfig.Get();
            _fixture = FixtureConfig.Get();
        }

        [Fact(DisplayName = "Lista Perfis")]
        public async Task GetAsync()
        {
            var entities = _fixture.Create<List<Perfil>>();

            _mockPerfilService.Setup(mock => mock.ObterTodosAsync()).ReturnsAsync(entities);

            var controller = new PerfilController(_mapper, _mockPerfilService.Object);

            var actionResult = await controller.GetAsync();

            var objectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var response = Assert.IsType<List<PerfilResponse>>(objectResult.Value);
            Assert.True(response.Count() > 0);
        }

        [Fact(DisplayName = "Busca Perfil Id")]
        public async Task GetById()
        {
            var entity = _fixture.Create<Perfil>();

            _mockPerfilService.Setup(mock => mock.ObterPorIdAsync(It.IsAny<int>())).ReturnsAsync(entity);

            var controller = new PerfilController(_mapper, _mockPerfilService.Object);

            var actionResult = await controller.GetByIdAsync(entity.Id);

            var objectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var response = Assert.IsType<PerfilResponse>(objectResult.Value);
            Assert.Equal(response.Id, entity.Id);
        }

        [Fact(DisplayName = "Cadastra Perfil")]
        public async Task Post()
        {
            var request = _fixture.Create<PerfilRequest>();

            _mockPerfilService.Setup(mock => mock.AdicionarAsync(It.IsAny<Perfil>())).Returns(Task.CompletedTask);

            var controller = new PerfilController(_mapper, _mockPerfilService.Object);

            var actionResult = await controller.PostAsync(request);

            var objectResult = Assert.IsType<CreatedResult>(actionResult);
            Assert.Equal(StatusCodes.Status201Created, objectResult.StatusCode);
        }

        [Fact(DisplayName = "Edita Perfil Existente")]
        public async Task Put()
        {
            var id = _fixture.Create<int>();
            var request = _fixture.Create<PerfilRequest>();

            _mockPerfilService.Setup(mock => mock.AlterarAsync(It.IsAny<Perfil>())).Returns(Task.CompletedTask);

            var controller = new PerfilController(_mapper, _mockPerfilService.Object);

            var actionResult = await controller.PutAsync(id, request);

            var objectResult = Assert.IsType<NoContentResult>(actionResult);
            Assert.Equal(StatusCodes.Status204NoContent, objectResult.StatusCode);
        }

        [Fact(DisplayName = "Remove Perfil Existente")]
        public async Task Delete()
        {
            var id = _fixture.Create<int>();

            _mockPerfilService.Setup(mock => mock.DeletarAsync(It.IsAny<int>())).Returns(Task.CompletedTask);

            var controller = new PerfilController(_mapper, _mockPerfilService.Object);

            var actionResult = await controller.DeleteAsync(id);

            var objectResult = Assert.IsType<NoContentResult>(actionResult);
            Assert.Equal(StatusCodes.Status204NoContent, objectResult.StatusCode);
        }
    }
}
