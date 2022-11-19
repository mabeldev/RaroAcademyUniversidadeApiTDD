using System.ComponentModel.DataAnnotations;

namespace demys_universidade.Domain.Contracts.Request
{
    public class CursoNomeRequest
    {
 
        [Required(ErrorMessage = "Nome do curso é obrigatório.")]
        public string Nome { get; set; }
    }
}
