using demys_universidade.Domain.Entities;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace demys_universidade.Domain.Contracts.Request
{
    public class AutenticacaoRequest
    {
        [Required(ErrorMessage = "O campo 'CPF' é obrigatorio")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "O campo 'Senha' é obrigatorio")]
        public string Senha { get; set; }

    }
}
