﻿using demys_universidade.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demys_universidade.Domain.Interfaces.Repositories
{
    public interface IBrasilCepRepository
    {
        Task<BrasilCep> GetCepAsync(string cep);
    }
}
