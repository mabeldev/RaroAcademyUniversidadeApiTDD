using AutoFixture;
using Bogus;
using demys_universidade.Domain.Entities;
using demys_universidade.Domain.Interfaces.Repositories;
using demys_universidade.Domain.Services;
using demys_universidade.Domain.Settings;
using demys_universidade.Test.Configs;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Linq.Expressions;

namespace demys_universidade.Test.Sources.Domain.Services
{
    [Trait("Service", "Service Curso")]
    public class CursoServiceTest
    {
        private readonly Mock<ICursoRepository> _mockCursoRepository;
        private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;
        private readonly Mock<AppSetting> _mockAppSetting;
        private readonly Faker _faker;
        private readonly Fixture _fixture;

        public CursoServiceTest()
        {
            _mockCursoRepository = new Mock<ICursoRepository>();
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            _faker = new Faker();
            _fixture = FixtureConfig.Get();
            _mockAppSetting = new Mock<AppSetting>();
        }

        [Theory(DisplayName = "Lista Cursos")]
        [InlineData("Aluno")]
        [InlineData("Professor")]
        public async Task Get(string perfil)
        {
            var entities = _fixture.Create<List<Curso>>();
            var claims = ClaimConfig.Get(_faker.UniqueIndex, _faker.Person.FullName, _faker.Person.Email, perfil);

            _mockCursoRepository.Setup(mock => mock.ListAsync(It.IsAny<Expression<Func<Curso, bool>>>())).ReturnsAsync(entities);
            _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);

            var service = new CursoService(_mockCursoRepository.Object, _mockHttpContextAccessor.Object);

            var response = await service.ObterTodosAsync();

            Assert.True(response.ToList().Count() > 0);
        }

        [Theory(DisplayName = "Busca Curso Id")]
        [InlineData("Aluno")]
        [InlineData("Professor")]
        public async Task GetById(string perfil)
        {
            var entity = _fixture.Create<Curso>();
            var claims = ClaimConfig.Get(_faker.UniqueIndex, _faker.Person.FullName, _faker.Person.Email, perfil);

            _mockCursoRepository.Setup(mock => mock.FindAsync(It.IsAny<Expression<Func<Curso, bool>>>())).ReturnsAsync(entity);
            _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);

            var service = new CursoService(_mockCursoRepository.Object, _mockHttpContextAccessor.Object);

            var response = await service.ObterPorIdAsync(entity.Id);

            Assert.Equal(response.Id, entity.Id);
        }        

        [Fact(DisplayName = "Cadastra Curso")]
        public async Task Post()
        {
            var entity = _fixture.Create<Curso>();

            _mockCursoRepository.Setup(mock => mock.AddAsync(It.IsAny<Curso>())).Returns(Task.CompletedTask);

            var service = new CursoService(_mockCursoRepository.Object, _mockHttpContextAccessor.Object);

            try
            {
                await service.AdicionarAsync(entity);
            }
            catch (Exception)
            {
                Assert.True(false);
            }
        }

        [Theory(DisplayName = "Edita Curso Existente")]
        [InlineData("Aluno")]
        [InlineData("Professor")]
        public async Task Put(string perfil)
        {
            var entity = _fixture.Create<Curso>();
            var claims = ClaimConfig.Get(_faker.UniqueIndex, _faker.Person.FullName, _faker.Person.Email, perfil);

            _mockCursoRepository.Setup(mock => mock.FindAsNoTrackingAsync(It.IsAny<Expression<Func<Curso, bool>>>())).ReturnsAsync(entity);
            _mockCursoRepository.Setup(mock => mock.EditAsync(It.IsAny<Curso>())).Returns(Task.CompletedTask);
            _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);

            var service = new CursoService(_mockCursoRepository.Object, _mockHttpContextAccessor.Object);

            try
            {
                await service.AlterarAsync(entity);
            }
            catch (Exception)
            {
                Assert.True(false);
            }
        }

        [Theory(DisplayName = "Remove Curso Existente")]
        [InlineData("Aluno")]
        [InlineData("Professor")]
        public async Task Delete(string perfil)
        {
            var entity = _fixture.Create<Curso>();
            var claims = ClaimConfig.Get(_faker.UniqueIndex, _faker.Person.FullName, _faker.Person.Email, perfil);

            _mockCursoRepository.Setup(mock => mock.FindAsync(It.IsAny<int>())).ReturnsAsync(entity);
            _mockCursoRepository.Setup(mock => mock.RemoveAsync(It.IsAny<Curso>())).Returns(Task.CompletedTask);
            _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);

            var service = new CursoService(_mockCursoRepository.Object, _mockHttpContextAccessor.Object);

            try
            {
                await service.DeletarAsync(entity.Id);
            }
            catch (Exception)
            {
                Assert.True(false);
            }
        }
    }
}