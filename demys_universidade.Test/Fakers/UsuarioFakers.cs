using Bogus;
using Bogus.Extensions.Brazil;
using demys_universidade.Domain.Contracts.Request;
using demys_universidade.Domain.Contracts.Response;
using demys_universidade.Domain.Entities;

namespace demys_universidade.Test.Fakers
{
    public class UsuarioFakers
    {
        private static readonly Faker faker = new Faker();

        public static int GetId()
        {
            return faker.Random.Int(1,10);
        }

        public static Usuario UsuarioFaker()
        {
            return new Usuario()
            {
                #region BaseEntity
                Id = faker.Random.Int(1, 10),
                Ativo = true,
                UsuarioInclusao = faker.Random.Int(1, 10),
                DataInclusao = DateTime.Now,
                UsuarioAlteracao = faker.Random.Int(1, 10),
                DataAlteracao = DateTime.Now,
                #endregion
                EnderecoId = faker.Random.Int(1, 10),
                Nome = faker.Person.FirstName.ToString(),
                Senha = faker.Internet.Password(),
                CPF = faker.Person.Cpf(),
                DataNascimento = faker.Person.DateOfBirth.ToLocalTime(),
                DataInicio = faker.Person.DateOfBirth.ToLocalTime(),
                CursoId = faker.Random.Int(1, 10),
                PerfilId = faker.Random.Int(1, 10),
            };
        }

        public static async Task<Usuario> UsuarioFakerId(int id)
        {
            return new Usuario()
            {
                #region BaseEntity
                Id = id,
                Ativo = true,
                UsuarioInclusao = faker.Random.Int(1, 10),
                DataInclusao = DateTime.Now,
                UsuarioAlteracao = faker.Random.Int(1, 10),
                DataAlteracao = DateTime.Now,
                #endregion
                EnderecoId = faker.Random.Int(1, 10),
                Nome = faker.Person.FirstName.ToString(),
                Senha = faker.Internet.Password(),
                CPF = faker.Person.Cpf(),
                DataNascimento = faker.Person.DateOfBirth.ToLocalTime(),
                DataInicio = faker.Person.DateOfBirth.ToLocalTime(),
                CursoId = faker.Random.Int(1, 10),
                PerfilId = faker.Random.Int(1, 10),
            };
        }

        public static async Task<Usuario> UsuarioFakerTask()
        {
            return new Usuario()
            {
                #region BaseEntity
                Id = faker.Random.Int(1, 10),
                Ativo = true,
                UsuarioInclusao = faker.Random.Int(1, 10),
                DataInclusao = DateTime.Now,
                UsuarioAlteracao = faker.Random.Int(1, 10),
                DataAlteracao = DateTime.Now,
                #endregion
                EnderecoId = faker.Random.Int(1, 10),
                Nome = faker.Person.FirstName.ToString(),
                Senha = faker.Internet.Password(),
                CPF = faker.Person.Cpf(),
                DataNascimento = faker.Person.DateOfBirth.ToLocalTime(),
                DataInicio = faker.Person.DateOfBirth.ToLocalTime(),
                CursoId = faker.Random.Int(1, 10),
                PerfilId = faker.Random.Int(1, 10),
            };
        }

        public static UsuarioResponse UsuarioResponseFaker()
        {
            return new UsuarioResponse()
            {
                Nome = faker.Person.FirstName.ToString(),
                CPF = faker.Person.Cpf(),
                Perfil = PerfilFakers.PerfilResponseFaker(),
                DataNascimento = faker.Person.DateOfBirth.ToLocalTime(),
                CursoId = faker.Random.Int(1, 10),
                Endereco = EnderecoFakers.EnderecoResponseFaker(),
                Id = faker.Random.Int(1, 10),
                Ativo = true,
            };
        }

        public static async Task<UsuarioResponse> UsuarioResponseFakerId(int id)
        {
            return new UsuarioResponse()
            {
                Nome = faker.Person.FirstName.ToString(),
                CPF = faker.Person.Cpf(),
                Perfil = PerfilFakers.PerfilResponseFaker(),
                DataNascimento = faker.Person.DateOfBirth.ToLocalTime(),
                CursoId = faker.Random.Int(1, 10),
                Endereco = EnderecoFakers.EnderecoResponseFaker(),
                Id = id,
                Ativo = true,
            };
        }

        public static UsuarioRequest UsuarioRequestFaker()
        {
            return new UsuarioRequest()
            {
                Nome = faker.Person.FirstName.ToString(),
                Senha = faker.Internet.Password(),
                CPF = faker.Person.Cpf(),
                PerfilId = faker.Random.Int(1, 10),
                DataNascimento = faker.Person.DateOfBirth.ToLocalTime(),
                CursoId = faker.Random.Int(1, 10),
                Endereco = EnderecoFakers.EnderecoRequestFaker(),
            };
        }
        public static UsuarioDataNascimentoRequest UsuarioDataRequestFaker()
        {
            return new UsuarioDataNascimentoRequest()
            {
                DataNascimento = faker.Person.DateOfBirth.ToLocalTime(),
            };
        }
    }
}
