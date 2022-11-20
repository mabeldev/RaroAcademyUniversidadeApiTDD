using AutoFixture;
using demys_universidade.Domain.Entities;
using demys_universidade.Infrastructure.Contexts;
using demys_universidade.Infrastructure.Repositories;
using demys_universidade.Test.Configs;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demys_universidade.Test.Sources.Infrastructure.Repositories
{
    public class PerfilRepositoryTest
    {
        private readonly Mock<UniversidadeContext> _mockContext;
        private readonly Fixture _fixture;

        public PerfilRepositoryTest()
        {
            _mockContext = new Mock<UniversidadeContext>(new DbContextOptionsBuilder<UniversidadeContext>().UseLazyLoadingProxies().Options);
            _fixture = FixtureConfig.Get();
        }

        [Fact(DisplayName = "Lista Perfils")]
        public async Task Get()
        {
            var entities = _fixture.Create<List<Perfil>>();

            _mockContext.Setup(mock => mock.Set<Perfil>()).ReturnsDbSet(entities);

            var repository = new PerfilRepository(_mockContext.Object);

            var response = await repository.ListAsync();

            Assert.True(response.Count() > 0);
        }

        [Fact(DisplayName = "Busca Perfil Id")]
        public async Task GetById()
        {
            var entity = _fixture.Create<Perfil>();

            _mockContext.Setup(mock => mock.Set<Perfil>().FindAsync(It.IsAny<int>())).ReturnsAsync(entity);

            var repository = new PerfilRepository(_mockContext.Object);

            var response = await repository.FindAsync(entity.Id);

            Assert.Equal(response.Id, entity.Id);
        }

        [Fact(DisplayName = "Cadastra Perfil")]
        public async Task Post()
        {
            var entity = _fixture.Create<Perfil>();

            _mockContext.Setup(mock => mock.Set<Perfil>()).ReturnsDbSet(new List<Perfil> { new Perfil() });

            var repository = new PerfilRepository(_mockContext.Object);

            var exception = await Record.ExceptionAsync(async () => await repository.AddAsync(entity));
            Assert.Null(exception);

        }

        [Fact(DisplayName = "Edita Perfil Existente")]
        public async Task Put()
        {
            var entity = _fixture.Create<Perfil>();

            _mockContext.Setup(mock => mock.Set<Perfil>()).ReturnsDbSet(new List<Perfil> { new Perfil() });

            var repository = new PerfilRepository(_mockContext.Object);

            var exception = await Record.ExceptionAsync(async () => await repository.EditAsync(entity));
            Assert.Null(exception);

        }

        [Fact(DisplayName = "Remove Perfil Existente")]
        public async Task Delete()
        {
            var entity = _fixture.Create<Perfil>();

            _mockContext.Setup(mock => mock.Set<Perfil>()).ReturnsDbSet(new List<Perfil> { entity });

            var repository = new PerfilRepository(_mockContext.Object);

            var exception = await Record.ExceptionAsync(async () => await repository.RemoveAsync(entity));
            Assert.Null(exception);
        }
    }
}
