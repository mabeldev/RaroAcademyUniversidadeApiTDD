using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demys_universidade.Domain.Contracts.Response
{
    public class EnderecoResponse : BaseResponse
    {
        public string CEP { get; set; }

        public string Rua { get; set; }

        public string Cidade { get; set; }

        public string Estado { get; set; }
    }
}
