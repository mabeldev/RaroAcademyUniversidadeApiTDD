using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demys_universidade.Domain.Contracts.Response
{
    public class BaseResponse
    {
        public int Id { get; set; }
        public bool Ativo { get; set; }
        public int? UsuarioInclusao { get; set; }
        public DateTime DataInclusao { get; set; }
        public int? UsuarioAlteracao { get; set; }
        public DateTime? DataAlteracao { get; set; }
    }
}
