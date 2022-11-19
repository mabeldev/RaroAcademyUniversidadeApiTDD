using demys_universidade.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demys_universidade.Domain.Interfaces.Services
{
    public interface IEnderecoService
    {
        Task<Endereco> GetPorRua(string rua);
        Task<BrasilCep> GetPorCep(string cep);
    }
}
