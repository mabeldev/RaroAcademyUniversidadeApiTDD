using demys_universidade.Domain.Enums;
using demys_universidade.Domain.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demys_universidade.Domain.Exceptions
{
    public class InformacaoException : Exception
    {
        public InformacaoException(StatusException status, List<string> mensagens, Exception exception = null)
           : base(status.Description(), exception)
        {
            Codigo = status;
            Mensagens = mensagens;
        }

        public InformacaoException(StatusException status, string mensagem, Exception exception = null)
            : base(status.Description(), exception)
        {
            Codigo = status;
            Mensagens = new List<string> { mensagem };
        }


        public StatusException Codigo { get; }

        public List<string> Mensagens { get; }
    }
}
