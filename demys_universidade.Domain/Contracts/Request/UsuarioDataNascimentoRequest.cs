using System.ComponentModel.DataAnnotations;

namespace demys_universidade.Domain.Contracts.Request
{
    public class UsuarioDataNascimentoRequest
    {

        [Required(ErrorMessage = "Data de Nascimento é obrigatório.")]
        public DateTime DataNascimento { get; set; }
    }
}
