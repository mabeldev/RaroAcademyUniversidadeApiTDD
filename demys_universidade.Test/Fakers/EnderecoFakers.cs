using Bogus;
using demys_universidade.Domain.Contracts.Request;
using demys_universidade.Domain.Contracts.Response;
using demys_universidade.Domain.Entities;

namespace demys_universidade.Test.Fakers
{
    public class EnderecoFakers
    {
        private static readonly Faker faker = new Faker();

        public static Endereco EnderecoFaker()
        {
            return new Endereco()
            {
                #region BaseEntity
                Id = faker.Random.Int(1, 10),
                Ativo = true,
                UsuarioInclusao = faker.Random.Int(1, 10),
                DataInclusao = DateTime.Now,
                UsuarioAlteracao = faker.Random.Int(1, 10),
                DataAlteracao = DateTime.Now,
                #endregion
                CEP = faker.Address.ZipCode("#####-###"),
                Cidade = faker.Address.City(),
                Estado = faker.Address.Country(),
                Rua = faker.Address.StreetName(),
            };
        }
        public static EnderecoResponse EnderecoResponseFaker()
        {
            return new EnderecoResponse()
            {
                Id = faker.Random.Int(),
                Ativo = true,
                CEP = faker.Address.ZipCode("#####-###"),
                Cidade = faker.Address.City(),
                Estado = faker.Address.Country(),
                Rua = faker.Address.StreetName(),
            };
        }
        public static EnderecoRequest EnderecoRequestFaker()
        {
            return new EnderecoRequest()
            {
                CEP = faker.Address.ZipCode("#####-###"),
                Cidade = faker.Address.City(),
                Estado = faker.Address.Country(),
                Rua = faker.Address.StreetName(),
            };
        }
    };
}
