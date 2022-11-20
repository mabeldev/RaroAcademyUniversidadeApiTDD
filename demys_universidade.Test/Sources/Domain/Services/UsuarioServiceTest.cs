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
    [Trait("Service", "Service Usuario")]
    public class UsuarioServiceTest
    {
        private readonly Mock<IUsuarioRepository> _mockUsuarioRepository;
        private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;
        private readonly Mock<AppSetting> _mockAppSetting;
        private readonly Faker _faker;
        private readonly Fixture _fixture;

        public UsuarioServiceTest()
        {
            _mockUsuarioRepository = new Mock<IUsuarioRepository>();
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            _faker = new Faker();
            _fixture = FixtureConfig.Get();
            _mockAppSetting = new Mock<AppSetting>();
        }

        [Theory(DisplayName = "Lista Usuarios")]
        [InlineData("Aluno")]
        [InlineData("Professor")]
        public async Task Get(string perfil)
        {
            var entities = _fixture.Create<List<Usuario>>();
            var claims = ClaimConfig.Get(_faker.UniqueIndex, _faker.Person.FullName, _faker.Person.Email, perfil);

            _mockUsuarioRepository.Setup(mock => mock.ListAsync(It.IsAny<Expression<Func<Usuario, bool>>>())).ReturnsAsync(entities);
            _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);

            var service = new UsuarioService(_mockUsuarioRepository.Object, _mockAppSetting.Object, _mockHttpContextAccessor.Object);

            var response = await service.ObterTodosAsync();

            Assert.True(response.ToList().Count() > 0);
        }

        [Theory(DisplayName = "Busca Usuario Id")]
        [InlineData("Aluno")]
        [InlineData("Professor")]
        public async Task GetById(string perfil)
        {
            var entity = _fixture.Create<Usuario>();
            var claims = ClaimConfig.Get(_faker.UniqueIndex, _faker.Person.FullName, _faker.Person.Email, perfil);

            _mockUsuarioRepository.Setup(mock => mock.FindAsync(It.IsAny<Expression<Func<Usuario, bool>>>())).ReturnsAsync(entity);
            _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);

            var service = new UsuarioService(_mockUsuarioRepository.Object, _mockAppSetting.Object, _mockHttpContextAccessor.Object);

            var response = await service.ObterPorIdAsync(entity.Id);

            Assert.Equal(response.Id, entity.Id);
        }        

        [Fact(DisplayName = "Cadastra Usuario")]
        public async Task Post()
        {
            var entity = _fixture.Create<Usuario>();

            _mockUsuarioRepository.Setup(mock => mock.AddAsync(It.IsAny<Usuario>())).Returns(Task.CompletedTask);

            var service = new UsuarioService(_mockUsuarioRepository.Object, _mockAppSetting.Object, _mockHttpContextAccessor.Object);

            try
            {
                await service.AdicionarAsync(entity);
            }
            catch (Exception)
            {
                Assert.True(false);
            }
        }

        [Theory(DisplayName = "Edita Usuario Existente")]
        [InlineData("Aluno")]
        [InlineData("Professor")]
        public async Task Put(string perfil)
        {
            var entity = _fixture.Create<Usuario>();
            var claims = ClaimConfig.Get(_faker.UniqueIndex, _faker.Person.FullName, _faker.Person.Email, perfil);

            _mockUsuarioRepository.Setup(mock => mock.FindAsNoTrackingAsync(It.IsAny<Expression<Func<Usuario, bool>>>())).ReturnsAsync(entity);
            _mockUsuarioRepository.Setup(mock => mock.EditAsync(It.IsAny<Usuario>())).Returns(Task.CompletedTask);
            _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);

            var service = new UsuarioService(_mockUsuarioRepository.Object, _mockAppSetting.Object, _mockHttpContextAccessor.Object);

            try
            {
                await service.AlterarAsync(entity);
            }
            catch (Exception)
            {
                Assert.True(false);
            }
        }

        [Theory(DisplayName = "Remove Usuario Existente")]
        [InlineData("Aluno")]
        [InlineData("Professor")]
        public async Task Delete(string perfil)
        {
            var entity = _fixture.Create<Usuario>();
            var claims = ClaimConfig.Get(_faker.UniqueIndex, _faker.Person.FullName, _faker.Person.Email, perfil);

            _mockUsuarioRepository.Setup(mock => mock.FindAsync(It.IsAny<int>())).ReturnsAsync(entity);
            _mockUsuarioRepository.Setup(mock => mock.RemoveAsync(It.IsAny<Usuario>())).Returns(Task.CompletedTask);
            _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);

            var service = new UsuarioService(_mockUsuarioRepository.Object, _mockAppSetting.Object, _mockHttpContextAccessor.Object);

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