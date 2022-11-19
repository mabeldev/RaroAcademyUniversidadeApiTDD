using demys_universidade.Domain.Contracts.Response;
using demys_universidade.Domain.Enums;
using demys_universidade.Domain.Exceptions;
using demys_universidade.Domain.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace demys_universidade.Filters
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        public override Task OnExceptionAsync(ExceptionContext context)
        {
            var response = new InformacaoResponse();

            if (context.Exception is InformacaoException)
            {
                var InformacaoException = (InformacaoException)context.Exception;

                response.Codigo = InformacaoException.Codigo;
                response.Mensagens = InformacaoException.Mensagens;
                response.Detalhe = context.Exception?.InnerException?.Message;
            }
            else
            {
                response.Codigo = StatusException.Erro;
                response.Mensagens.Add("Erro Inesperado");
                response.Mensagens = new List<string> { "Erro Inesperado"};
                response.Detalhe = context.Exception?.InnerException?.Message;
            }

            context.Result = new ObjectResult(response)
            {
                StatusCode = response.Codigo.GetStatusCode()
            };

            OnException(context);
            return Task.CompletedTask;
        }
    }
}
