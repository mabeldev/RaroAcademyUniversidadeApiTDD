namespace demys_universidade.Domain.Entities
{
    public class Endereco : BaseEntity
    {
        public string CEP { get; set; }
        public string Rua { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
    }
}
