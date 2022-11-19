using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demys_universidade.Domain.Contracts.Request
{
    public class EnderecoRequest
    {
        [Required(ErrorMessage = "CEP é obrigatório.")]
        [RegularExpression(@"(^[0-9]{5})-?([0-9]{3}$)")]
        public string CEP { get; set; }

        [Required(ErrorMessage = "Rua é obrigatória.")]
        public string Rua { get; set; }

        [Required(ErrorMessage = "Cidade é obrigatória.")]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "Estado é obrigatório.")]
        public string Estado { get; set; }
    }
}
