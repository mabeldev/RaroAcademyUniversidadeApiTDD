using demys_universidade.Domain.Enums;
using demys_universidade.Domain.Utils;

namespace demys_universidade.Domain.Contracts.Response
{
    public class InformacaoResponse
    {
        public StatusException Codigo { get; set; }
        public string Descricao { get { return Codigo.Description(); } }
        public List<string> Mensagens { get; set; }
        public string Detalhe { get; set; }
    }
}
