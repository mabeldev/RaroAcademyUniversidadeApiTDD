using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demys_universidade.Domain.Entities
{
    public class Curso : BaseEntity
    {
        public string Nome { get; set; }
        public string Turno { get; set; }

        #region ForingKeyId
        public int DepartamentoId { get; set; }

        #endregion

        #region ForingKeyReference
        public virtual Departamento Departamento { get; set; }
        public virtual ICollection<Usuario> Usuarios { get; set; }
        #endregion
    }
}
