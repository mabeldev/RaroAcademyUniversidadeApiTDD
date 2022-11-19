using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demys_universidade.Domain.Contracts.Response
{
    public class CursoResponse : BaseResponse
    {
        public int DepartamentoId { get; set; }

        public string Nome { get; set; }

        public string Turno { get; set; }
    }
}
