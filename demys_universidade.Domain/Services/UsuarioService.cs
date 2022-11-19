using demys_universidade.Domain.Contracts.Response;
using demys_universidade.Domain.Entities;
using demys_universidade.Domain.Exceptions;
using demys_universidade.Domain.Interfaces.Repositories;
using demys_universidade.Domain.Interfaces.Services;
using demys_universidade.Domain.Settings;
using demys_universidade.Domain.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace demys_universidade.Domain.Services
{
    public class UsuarioService : BaseService<Usuario>, IUsuarioService
    {

        #region Constructor
        private readonly AppSetting _appSettings;
        private readonly IUsuarioRepository _usuarioRepository;


        public UsuarioService(IUsuarioRepository usuarioRepository,
                                AppSetting appSettings,
                                    IHttpContextAccessor httpContextAccessor)
                                        :base(usuarioRepository, httpContextAccessor)
        {
            _usuarioRepository = usuarioRepository;
            _appSettings = appSettings;
        }

        #endregion

        #region Atutenticacao
        public async Task<AutenticacaoResponse> AutenticarAsync(string cpf, string senha)
        {
            var entity = await ObterAsync(x => x.CPF.Equals(cpf) && x.Ativo);

            if (!BCrypt.Net.BCrypt.Verify(senha, entity.Senha))
                throw new InformacaoException(Enums.StatusException.FormatoIncorreto, "Usuário ou senha incorreta");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, entity.Id.ToString()),
                    new Claim(ClaimTypes.Name, entity.Nome),
                    new Claim(ClaimTypes.Email, entity.CPF),
                    new Claim(ClaimTypes.Role, entity.Perfil.Nome)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_appSettings.JwtSecurityKey)),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return new AutenticacaoResponse
            {
                Token = tokenString,
                DataExpiracao = tokenDescriptor.Expires
            };
        }
        #endregion

        #region Criar Usuario
        public async Task CriarUsuarioAsync(Usuario usuario)
        {
            usuario.Senha = BCrypt.Net.BCrypt.HashPassword(usuario.Senha, BCrypt.Net.BCrypt.GenerateSalt());
            await AdicionarAsync(usuario);
        }

        #endregion

        #region AtualizarUsuario
        public async Task AtualizarUsuarioAsync(Usuario usuario)
        {
            usuario.Senha = BCrypt.Net.BCrypt.HashPassword(usuario.Senha, BCrypt.Net.BCrypt.GenerateSalt());
            await AlterarAsync(usuario);
        }

        #endregion

        #region Atualizar Data De Nascimento
        public async Task AtualizarDataNascimentoAsync(int id, DateTime dataNascimento)
        {
            var entity = await ObterPorIdAsync(id);
            entity.DataNascimento = dataNascimento;
            entity.DataAlteracao = DateTime.Now;
            entity.UsuarioAlteracao = UserId;
            await _usuarioRepository.EditAsync(entity);
        }

        #endregion

        #region Obter Todos os Usuarios
        public async Task<List<Usuario>> ObterTodosUsuarioAsync()
        {
            if (UserPerfil == ConstanteUtil.PerfilAlunoNome)
                return await ObterTodosAsync(x => x.Ativo && x.Id == UserId);
            else
                return await ObterTodosAsync();
        }

        #endregion

        #region Obter Usuarios por Id
        public async Task<Usuario> ObterPorIdUsuarioAsync(int id)
        {
            if (UserPerfil == ConstanteUtil.PerfilAlunoNome)
                return await ObterAsync(x => x.Id == id && x.Ativo && x.Id == UserId);
            else
                return await ObterPorIdAsync(id);
        }

        #endregion
    }
}
