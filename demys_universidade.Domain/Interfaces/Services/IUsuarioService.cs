using demys_universidade.Domain.Contracts.Response;
using demys_universidade.Domain.Entities;

namespace demys_universidade.Domain.Interfaces.Services
{
    public interface IUsuarioService : IBaseService<Usuario>
    {
        Task<AutenticacaoResponse> AutenticarAsync(string cpf, string senha);
        Task CriarUsuarioAsync(Usuario usuario);
        Task AtualizarUsuarioAsync(Usuario usuario);
        Task AtualizarDataNascimentoAsync(int id, DateTime dataDeNascimento);
        Task<List<Usuario>> ObterTodosUsuarioAsync();
        Task<Usuario> ObterPorIdUsuarioAsync(int id);
    }
}
