using AutoFixture;
using demys_universidade.Domain.Entities;
using demys_universidade.Infrastructure.Contexts;
using demys_universidade.Infrastructure.Repositories;
using demys_universidade.Test.Configs;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;

namespace demys_universidade.Test.Sources.Infrastructure.Repositories
{
    [Trait("Repository", "Repository Endereco")]
    public class EnderecoRepositoryTest
    {
        private readonly Mock<UniversidadeContext> _mockContext;
        private readonly Fixture _fixture;

        public EnderecoRepositoryTest()
        {
            _mockContext = new Mock<UniversidadeContext>(new DbContextOptionsBuilder<UniversidadeContext>().UseLazyLoadingProxies().Options);
            _fixture = FixtureConfig.Get();
        }

        [Fact(DisplayName = "Lista Enderecos")]
        public async Task Get()
        {
            var entities = _fixture.Create<List<Endereco>>();

            _mockContext.Setup(mock => mock.Set<Endereco>()).ReturnsDbSet(entities);

            var repository = new EnderecoRepository(_mockContext.Object);

            var response = await repository.ListAsync();

            Assert.True(response.Count() > 0);
        }

        [Fact(DisplayName = "Busca Endereco Id")]
        public async Task GetById()
        {
            var entity = _fixture.Create<Endereco>();

            _mockContext.Setup(mock => mock.Set<Endereco>().FindAsync(It.IsAny<int>())).ReturnsAsync(entity);

            var repository = new EnderecoRepository(_mockContext.Object);

            var response = await repository.FindAsync(entity.Id);

            Assert.Equal(response.Id, entity.Id);
        }

        [Fact(DisplayName = "Cadastra Endereco")]
        public async Task Post()
        {
            var entity = _fixture.Create<Endereco>();

            _mockContext.Setup(mock => mock.Set<Endereco>()).ReturnsDbSet(new List<Endereco> { new Endereco() });

            var repository = new EnderecoRepository(_mockContext.Object);

            var exception = await Record.ExceptionAsync(async () => await repository.AddAsync(entity));
            Assert.Null(exception);

        }

        [Fact(DisplayName = "Edita Endereco Existente")]
        public async Task Put()
        {
            var entity = _fixture.Create<Endereco>();

            _mockContext.Setup(mock => mock.Set<Endereco>()).ReturnsDbSet(new List<Endereco> { new Endereco() });

            var repository = new EnderecoRepository(_mockContext.Object);

            var exception = await Record.ExceptionAsync(async () => await repository.EditAsync(entity));
            Assert.Null(exception);

        }

        [Fact(DisplayName = "Remove Endereco Existente")]
        public async Task Delete()
        {
            var entity = _fixture.Create<Endereco>();

            _mockContext.Setup(mock => mock.Set<Endereco>()).ReturnsDbSet(new List<Endereco> { entity });

            var repository = new EnderecoRepository(_mockContext.Object);

            var exception = await Record.ExceptionAsync(async () => await repository.RemoveAsync(entity));
            Assert.Null(exception);


        }

    }
}