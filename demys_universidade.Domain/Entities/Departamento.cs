using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demys_universidade.Domain.Entities
{
    public class Departamento : BaseEntity
    {
        public string Nome { get; set; }

        #region ForingKeyId
        public int EnderecoId { get; set; }

        #endregion
        #region ForingKeyReference
        public virtual Endereco Endereco { get; set; }

        #endregion
    }
}
