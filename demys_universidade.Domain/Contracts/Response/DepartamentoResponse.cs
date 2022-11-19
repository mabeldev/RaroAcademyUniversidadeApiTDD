using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demys_universidade.Domain.Contracts.Response
{
    public class DepartamentoResponse : BaseResponse
    {
        public string Nome { get; set; }
        public EnderecoResponse Endereco { get; set; }

    }
}
