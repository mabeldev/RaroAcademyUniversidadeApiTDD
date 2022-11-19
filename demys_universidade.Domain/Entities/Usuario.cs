namespace demys_universidade.Domain.Entities
{
    public class Usuario : BaseEntity
    {
        public string Nome { get; set; }
        public string Senha { get; set; }
        public string CPF { get; set; }
        public DateTime DataNascimento { get; set; }
        public DateTime DataInicio { get; set;  }

        #region ForingKeyId
        public int CursoId { get; set; }
        public int EnderecoId { get; set; }
        public int PerfilId { get; set; }

        #endregion

        #region ForingKeyReference
        public virtual Curso Curso { get; set; }
        public virtual Endereco Endereco { get; set; }
        public virtual Perfil Perfil { get; set; }

        #endregion
    }
}
