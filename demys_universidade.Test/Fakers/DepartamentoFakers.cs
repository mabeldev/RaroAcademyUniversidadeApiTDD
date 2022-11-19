using Bogus;
using demys_universidade.Domain.Contracts.Request;
using demys_universidade.Domain.Contracts.Response;
using demys_universidade.Domain.Entities;

namespace demys_universidade.Test.Fakers
{
    public class DepartamentoFakers
    {

        private static readonly Faker faker = new Faker();

        public static Departamento DepartamentoFaker()
        {
            return new Departamento()
            {
                #region BaseEntity
                Id = faker.Random.Int(1, 10),
                Ativo = true,
                UsuarioInclusao = faker.Random.Int(1, 10),
                DataInclusao = DateTime.Now,
                UsuarioAlteracao = faker.Random.Int(1, 10),
                DataAlteracao = DateTime.Now,
                #endregion
                Endereco = EnderecoFakers.EnderecoFaker(),
                EnderecoId = faker.Random.Int(1, 10),
                Nome = faker.Name.JobTitle(),
            };
        }
        public static DepartamentoResponse DepartamentoResponseFaker()
        {
            return new DepartamentoResponse()
            {
                Id = faker.Random.Int(),
                Ativo = true,
                Endereco = EnderecoFakers.EnderecoResponseFaker(),
                Nome = faker.Name.JobTitle(),
            };
        }
        public static DepartamentoRequest DepartamentoRequestFaker()
        {
            return new DepartamentoRequest()
            {
                Endereco = EnderecoFakers.EnderecoRequestFaker(),
                Nome = faker.Name.JobTitle(),
            };
        }
    };
}
