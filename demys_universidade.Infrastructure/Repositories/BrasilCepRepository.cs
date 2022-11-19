using demys_universidade.Domain.Entities;
using demys_universidade.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demys_universidade.Infrastructure.Repositories
{
    public class BrasilCepRepository : BaseApiRepository, IBrasilCepRepository
    {
        public BrasilCepRepository(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task<BrasilCep> GetCepAsync(string cep)
        {
            return await GetAsync<BrasilCep>($"{cep}");
        }
    }
}