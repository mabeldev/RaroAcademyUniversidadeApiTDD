using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demys_universidade.Domain.Utils
{
    public static class ConstanteUtil
    {
        public const string PerfilAlunoNome = "Aluno";
        public const string PerfilProfessorNome = "Professor";
        public const string PerfilLogadoNome = $"{PerfilAlunoNome}, {PerfilProfessorNome}";

    }
}
