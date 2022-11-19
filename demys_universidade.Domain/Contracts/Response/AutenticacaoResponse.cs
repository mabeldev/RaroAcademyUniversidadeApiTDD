using demys_universidade.Domain.Entities;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace demys_universidade.Domain.Contracts.Response
{
    public class AutenticacaoResponse
    {
        public string Token { get; set; }
        public DateTime? DataExpiracao { get; set; }
    }
}
