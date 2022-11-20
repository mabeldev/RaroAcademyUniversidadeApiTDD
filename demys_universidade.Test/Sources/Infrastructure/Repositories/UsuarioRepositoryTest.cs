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
    [Trait("Repository", "Repository Usuario")]
    public class UsuarioRepositoryTest
    {
        private readonly Mock<UniversidadeContext> _mockContext;
        private readonly Fixture _fixture;

        public UsuarioRepositoryTest()
        {
            _mockContext = new Mock<UniversidadeContext>(new DbContextOptionsBuilder<UniversidadeContext>().UseLazyLoadingProxies().Options);
            _fixture = FixtureConfig.Get();
        }

        [Fact(DisplayName = "Lista Usuarios")]
        public async Task Get()
        {
            var entities = _fixture.Create<List<Usuario>>();

            _mockContext.Setup(mock => mock.Set<Usuario>()).ReturnsDbSet(entities);

            var repository = new UsuarioRepository(_mockContext.Object);

            var response = await repository.ListAsync();

            Assert.True(response.Count() > 0);
        }

        [Fact(DisplayName = "Busca Usuario Id")]
        public async Task GetById()
        {
            var entity = _fixture.Create<Usuario>();

            _mockContext.Setup(mock => mock.Set<Usuario>().FindAsync(It.IsAny<int>())).ReturnsAsync(entity);

            var repository = new UsuarioRepository(_mockContext.Object);

            var response = await repository.FindAsync(entity.Id);

            Assert.Equal(response.Id, entity.Id);
        }

        [Fact(DisplayName = "Cadastra Usuario")]
        public async Task Post()
        {
            var entity = _fixture.Create<Usuario>();

            _mockContext.Setup(mock => mock.Set<Usuario>()).ReturnsDbSet(new List<Usuario> { new Usuario() });

            var repository = new UsuarioRepository(_mockContext.Object);

            var exception = await Record.ExceptionAsync(async () => await repository.AddAsync(entity));
            Assert.Null(exception);

        }

        [Fact(DisplayName = "Edita Usuario Existente")]
        public async Task Put()
        {
            var entity = _fixture.Create<Usuario>();

            _mockContext.Setup(mock => mock.Set<Usuario>()).ReturnsDbSet(new List<Usuario> { new Usuario() });

            var repository = new UsuarioRepository(_mockContext.Object);

            var exception = await Record.ExceptionAsync(async () => await repository.EditAsync(entity));
            Assert.Null(exception);

        }



        [Fact(DisplayName = "Remove Usuario Existente")]
        public async Task Delete()
        {
            var entity = _fixture.Create<Usuario>();

            _mockContext.Setup(mock => mock.Set<Usuario>()).ReturnsDbSet(new List<Usuario> { entity });

            var repository = new UsuarioRepository(_mockContext.Object);

            var exception = await Record.ExceptionAsync(async () => await repository.RemoveAsync(entity));
            Assert.Null(exception);


        }

    }
}