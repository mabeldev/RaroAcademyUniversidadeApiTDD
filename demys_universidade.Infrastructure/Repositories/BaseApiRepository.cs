using System.Net.Http.Json;
using System.Net;
using System.Net.Http.Formatting;
using demys_universidade.Domain.Exceptions;
using demys_universidade.Domain.Enums;

namespace demys_universidade.Infrastructure.Repositories
{
    public class BaseApiRepository
    {
        public readonly HttpClient _httpClient;

        public BaseApiRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<T> GetAsync<T>(string uri)
        {
            var response = await _httpClient.GetAsync(uri);

            if (!response.IsSuccessStatusCode)
                await TratarErroAsync(response);

            return await ReadAsync<T>(response);
        }

        private async Task TratarErroAsync(HttpResponseMessage response)
        {
            var body = await ReadAsync<string>(response);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
                throw new InformacaoException(StatusException.NaoAutorizado, $"Usuário sem acesso. {body}");
            if (response.StatusCode == HttpStatusCode.Forbidden)
                throw new InformacaoException(StatusException.AcessoProibido, $"Usuário sem permissão. {body}");
            if (response.StatusCode == HttpStatusCode.BadRequest)
                throw new InformacaoException(StatusException.FormatoIncorreto, $"Requisição inválida. {body}");
            else
                throw new InformacaoException(StatusException.Erro, $"Erro ao fazer a requisição. StatusCode: {response.StatusCode} {body}");
        }

        private async Task<T> ReadAsync<T>(HttpResponseMessage response)
        {
            return await response.Content.ReadAsAsync<T>();
        }
    }
}
