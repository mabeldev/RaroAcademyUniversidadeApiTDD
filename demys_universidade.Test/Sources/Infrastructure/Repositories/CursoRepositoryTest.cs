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
    [Trait("Repository", "Repository Curso")]
    public class CursoRepositoryTest
    {
        private readonly Mock<UniversidadeContext> _mockContext;
        private readonly Fixture _fixture;

        public CursoRepositoryTest()
        {
            _mockContext = new Mock<UniversidadeContext>(new DbContextOptionsBuilder<UniversidadeContext>().UseLazyLoadingProxies().Options);
            _fixture = FixtureConfig.Get();
        }

        [Fact(DisplayName = "Lista Cursos")]
        public async Task Get()
        {
            var entities = _fixture.Create<List<Curso>>();

            _mockContext.Setup(mock => mock.Set<Curso>()).ReturnsDbSet(entities);

            var repository = new CursoRepository(_mockContext.Object);

            var response = await repository.ListAsync();

            Assert.True(response.Count() > 0);
        }

        [Fact(DisplayName = "Busca Curso Id")]
        public async Task GetById()
        {
            var entity = _fixture.Create<Curso>();

            _mockContext.Setup(mock => mock.Set<Curso>().FindAsync(It.IsAny<int>())).ReturnsAsync(entity);

            var repository = new CursoRepository(_mockContext.Object);

            var response = await repository.FindAsync(entity.Id);

            Assert.Equal(response.Id, entity.Id);
        }

        [Fact(DisplayName = "Cadastra Curso")]
        public async Task Post()
        {
            var entity = _fixture.Create<Curso>();

            _mockContext.Setup(mock => mock.Set<Curso>()).ReturnsDbSet(new List<Curso> { new Curso() });

            var repository = new CursoRepository(_mockContext.Object);

            var exception = await Record.ExceptionAsync(async () => await repository.AddAsync(entity));
            Assert.Null(exception);

        }


        [Fact(DisplayName = "Edita Curso Existente")]
        public async Task Put()
        {
            var entity = _fixture.Create<Curso>();

            _mockContext.Setup(mock => mock.Set<Curso>()).ReturnsDbSet(new List<Curso> { new Curso() });

            var repository = new CursoRepository(_mockContext.Object);

            var exception = await Record.ExceptionAsync(async () => await repository.EditAsync(entity));
            Assert.Null(exception);

        }

        [Fact(DisplayName = "Remove Curso Existente")]
        public async Task Delete()
        {
            var entity = _fixture.Create<Curso>();

            _mockContext.Setup(mock => mock.Set<Curso>()).ReturnsDbSet(new List<Curso> { entity });

            var repository = new CursoRepository(_mockContext.Object);

            var exception = await Record.ExceptionAsync(async () => await repository.RemoveAsync(entity));
            Assert.Null(exception);


        }

    }
}