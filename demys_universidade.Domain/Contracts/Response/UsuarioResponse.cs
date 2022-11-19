using demys_universidade.Domain.Contracts.Request;
using demys_universidade.Domain.Entities;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demys_universidade.Domain.Contracts.Response
{
    public class UsuarioResponse : BaseResponse
    {
        public string Nome { get; set; }

        public string CPF { get; set; }

        public PerfilResponse Perfil { get; set; }

        public DateTime DataNascimento { get; set; }

        public int CursoId { get; set; }

        public EnderecoResponse Endereco { get; set; }
    }
}
