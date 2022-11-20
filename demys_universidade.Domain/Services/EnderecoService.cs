using demys_universidade.Domain.Entities;
using demys_universidade.Domain.Interfaces.Repositories;
using demys_universidade.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demys_universidade.Domain.Services
{
    public class EnderecoService : BaseService<Endereco>, IEnderecoService
    {
            private readonly IBrasilCepRepository _brasilCepRepository;
            private readonly IEnderecoRepository _repository;

            public EnderecoService(
                IEnderecoRepository enderecoRepository,
                IHttpContextAccessor httpContextAccessor,
                IBrasilCepRepository brasilCepRepository) : base(enderecoRepository, httpContextAccessor)
            {
                _brasilCepRepository = brasilCepRepository;
                _repository = enderecoRepository;
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
