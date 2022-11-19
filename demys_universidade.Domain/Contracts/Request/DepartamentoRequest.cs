using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demys_universidade.Domain.Contracts.Request
{
    public class DepartamentoRequest
    {
        [Required(ErrorMessage = "Nome do departamento é obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Endereco é obrigatório.")]
        public EnderecoRequest Endereco { get; set; }

    }
}
