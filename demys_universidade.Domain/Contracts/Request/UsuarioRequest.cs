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
    public class UsuarioRequest
    {
        [Required(ErrorMessage = "Nome é obrigatório.")]
        [StringLength(40, MinimumLength = 3)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Senha é obrigatória.")]
        [StringLength(18, MinimumLength = 6)]
        [RegularExpression(@"^((?=.*[a-z])(?=.*[A-Z])(?=.*\d)).+$")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [Required(ErrorMessage = "CPF é obrigatório.")]
        [RegularExpression(@"[0-9]{3}\.?[0-9]{3}\.?[0-9]{3}\-?[0-9]{2}")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "Perfil é obrigatório.")]
        public int PerfilId { get; set; }

        [Required(ErrorMessage = "Data de Nascimento é obrigatório.")]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "CursoId é obrigatório em usuário.")]
        public int CursoId { get; set; }

        [Required(ErrorMessage = "Endereco é obrigatório.")]
        public EnderecoRequest Endereco { get; set; }
    }
}
