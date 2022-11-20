using AutoFixture;
using demys_universidade.Domain.Entities;
using demys_universidade.Domain.Interfaces.Repositories;
using demys_universidade.Domain.Services;
using demys_universidade.Test.Configs;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace demys_universidade.Test.Sources.Domain.Services
{
    [Trait("Service", "Service Endereco")]
    public class EnderecoServiceTest
    {
        private readonly Mock<IEnderecoRepository> _mockEnderecoRepository;
        private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;
        private readonly Mock<IBrasilCepRepository> _mockBrasilCepRepository;
        private readonly Fixture _fixture;
        private readonly Claim[] _claims;

        public EnderecoServiceTest()
        {
            _mockEnderecoRepository = new Mock<IEnderecoRepository>();
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            _mockBrasilCepRepository = new Mock<IBrasilCepRepository>();
            _fixture = FixtureConfig.Get();
            _claims = _fixture.Create<Usuario>().Claims();
        }

        [Fact(DisplayName = "Lista Enderecos")]
        public async Task Get()
        {
            var entities = _fixture.Create<List<Endereco>>();

            _mockEnderecoRepository.Setup(mock => mock.ListAsync(It.IsAny<Expression<Func<Endereco, bool>>>())).ReturnsAsync(entities);
            _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(_claims);

            var service = new EnderecoService(_mockEnderecoRepository.Object, _mockHttpContextAccessor.Object, _mockBrasilCepRepository.Object);

            var response = await service.ObterTodosAsync();

            Assert.True(response.ToList().Count() > 0);
        }

        [Fact(DisplayName = "Busca Endereco Id")]
        public async Task GetById()
        {
            var entity = _fixture.Create<Endereco>();

            _mockEnderecoRepository.Setup(mock => mock.FindAsync(It.IsAny<Expression<Func<Endereco, bool>>>())).ReturnsAsync(entity);
            _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(_claims);

            var service = new EnderecoService(_mockEnderecoRepository.Object, _mockHttpContextAccessor.Object, _mockBrasilCepRepository.Object);

            var response = await service.ObterPorIdAsync(entity.Id);

            Assert.Equal(response.Id, entity.Id);
        }

        [Fact(DisplayName = "Cadastra Endereco")]
        public async Task Post()
        {
            var entity = _fixture.Create<Endereco>();

            _mockEnderecoRepository.Setup(mock => mock.AddAsync(It.IsAny<Endereco>())).Returns(Task.CompletedTask);
            _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(_claims);

            var service = new EnderecoService(_mockEnderecoRepository.Object, _mockHttpContextAccessor.Object, _mockBrasilCepRepository.Object);

            var exception = await Record.ExceptionAsync(async () => await service.AdicionarAsync(entity));
            Assert.Null(exception);

        }

        [Fact(DisplayName = "Edita Endereco Existente")]
        public async Task Put()
        {
            var entity = _fixture.Create<Endereco>();

            _mockEnderecoRepository.Setup(mock => mock.FindAsNoTrackingAsync(It.IsAny<Expression<Func<Endereco, bool>>>())).ReturnsAsync(entity);
            _mockEnderecoRepository.Setup(mock => mock.EditAsync(It.IsAny<Endereco>())).Returns(Task.CompletedTask);
            _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(_claims);

            var service = new EnderecoService(_mockEnderecoRepository.Object, _mockHttpContextAccessor.Object, _mockBrasilCepRepository.Object);

            var exception = await Record.ExceptionAsync(async () => await service.AlterarAsync(entity));
            Assert.Null(exception);

        }

        [Fact(DisplayName = "Remove endereco Existente")]
        public async Task Delete()
        {
            var entity = _fixture.Create<Endereco>();

            _mockEnderecoRepository.Setup(mock => mock.FindAsync(It.IsAny<int>())).ReturnsAsync(entity);
            _mockEnderecoRepository.Setup(mock => mock.RemoveAsync(It.IsAny<Endereco>())).Returns(Task.CompletedTask);
            _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(_claims);

            var service = new EnderecoService(_mockEnderecoRepository.Object, _mockHttpContextAccessor.Object, _mockBrasilCepRepository.Object);

            var exception = await Record.ExceptionAsync(async () => await service.DeletarAsync(entity.Id));
            Assert.Null(exception);

        }
    }

}
