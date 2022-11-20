using Bogus;
using demys_universidade.Domain.Contracts.Request;
using demys_universidade.Domain.Contracts.Response;
using demys_universidade.Domain.Entities;

namespace demys_universidade.Test.Fakers
{
    public class CursoFakers
    {

        private static readonly Faker faker = new Faker();

        public static Curso CursoFaker()
        {
            return new Curso()
            {
                #region BaseEntity
                Id = faker.Random.Int(1, 10),
                Ativo = true,
                UsuarioInclusao = faker.Random.Int(1, 10),
                DataInclusao = DateTime.Now,
                UsuarioAlteracao = faker.Random.Int(1, 10),
                DataAlteracao = DateTime.Now,
                #endregion
                Departamento = DepartamentoFakers.DepartamentoFaker(),
                DepartamentoId = faker.Random.Int(1, 10),
                Turno = faker.Person.FirstName.ToString(),
                Nome = faker.Person.LastName.ToString(),
                Usuarios = new[] {UsuarioFakers.UsuarioFaker()}
            };
        }
        public static CursoResponse CursoResponseFaker()
        {
            return new CursoResponse()
            {
                Id = faker.Random.Int(),
                Ativo = true,
                DepartamentoId = faker.Random.Int(1, 10), 
                Nome = faker.Person.FirstName.ToString(),
                Turno = faker.Person.FirstName.ToString()
            };
        }
        public static CursoRequest CursoRequestFaker()
        {
            return new CursoRequest()
            {
              DepartamentoId = faker.Random.Int(1,10),
              Nome= faker.Person.FirstName.ToString(),
              Turno= faker.Person.FirstName.ToString(),
            };
        }
        public static CursoNomeRequest CursoNomeRequestFaker()
        {
            return new CursoNomeRequest()
            {
                Nome = faker.Person.FirstName.ToString(),
            };
        }
    };
}
