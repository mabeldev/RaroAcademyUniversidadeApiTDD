using demys_universidade.Domain.Entities;
using demys_universidade.Domain.Interfaces.Repositories;
using demys_universidade.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demys_universidade.Domain.Services
{
    public class EnderecoService : IEnderecoService
    {
        private readonly IEnderecoRepository _repository;
        private readonly IBrasilCepRepository _brasilCepRepository;

        public EnderecoService(IEnderecoRepository repository, IBrasilCepRepository brasilCepRepository)
        {
            _repository = repository;
            _brasilCepRepository = brasilCepRepository;
        }

        public async Task<BrasilCep> GetPorCep(string cep)
        {
            return await _brasilCepRepository.GetCepAsync(cep);
        }

        public async Task<Endereco> GetPorRua(string rua)
        {
            return await _repository.FindAsync(p => p.Rua == rua);
        }

    }
}
