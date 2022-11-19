using System.ComponentModel.DataAnnotations;

namespace demys_universidade.Domain.Contracts.Request
{
    public class CursoRequest
    {
        [Required(ErrorMessage = "Id do departamento é obrigatório.")]
        public int DepartamentoId { get; set; }

        [Required(ErrorMessage = "Nome do curso é obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Turno do curso é obrigatório.")]
        public string Turno { get; set; }
    }
}
