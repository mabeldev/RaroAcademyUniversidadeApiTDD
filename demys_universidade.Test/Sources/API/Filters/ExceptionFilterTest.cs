using AutoFixture;
using demys_universidade.Domain.Enums;
using demys_universidade.Domain.Exceptions;
using demys_universidade.Filters;
using demys_universidade.Test.Configs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc.Abstractions;
using System;

namespace demys_universidade.Test.Sources.API.Filters
{
    public class ExceptionFilterTest
    {
        private readonly Fixture _fixture;
        private readonly ActionContext _actionContext;
        private readonly List<IFilterMetadata> _filterMetadata;

        public ExceptionFilterTest()
        {
            _fixture = FixtureConfig.Get();
            _actionContext = new ActionContext
            {
                ActionDescriptor = new ActionDescriptor(),
                HttpContext = new DefaultHttpContext(),
                RouteData = new RouteData()
            };
            _filterMetadata = new List<IFilterMetadata>();
        }

        [Fact(DisplayName = "Acionar uma Informacao Excepton")]
        public async void OnExceptionInformacaoException()
        {
            var exceptionContext = new ExceptionContext(_actionContext, _filterMetadata)
            {
                Exception = new InformacaoException(Domain.Enums.StatusException.NaoEncontrado, "Nenhum dado encontrado.")
            };

            var exceptionFilter = new ExceptionFilter();

            var result = await Record.ExceptionAsync(() => exceptionFilter.OnExceptionAsync(exceptionContext));
            Assert.Null(result);
        }

        [Fact(DisplayName = "Acionar uma Informacao Excepton")]
        public async void OnExceptionIsException()
        {
            var exceptionContext = new ExceptionContext(_actionContext, _filterMetadata)
            {
                Exception = new Exception("Erro inesperado.")
            };

            var exceptionFilter = new ExceptionFilter();

            var result = await Record.ExceptionAsync(() => exceptionFilter.OnExceptionAsync(exceptionContext));
            Assert.Null(result);
        }

        [Fact(DisplayName = "Informacao Exception Inner Exception")]
        public async Task OnExceptionFilterInformacaoExceptionInnerException()
        {
            var actionContext = new ActionContext()
            {
                HttpContext = new DefaultHttpContext(),
                RouteData = new RouteData(),
                ActionDescriptor = new ActionDescriptor()
            };

            var exceptionContext = new ExceptionContext(actionContext, new List<IFilterMetadata>())
            {
                Exception = new InformacaoException(StatusException.NaoEncontrado, "Nenhum dado encontrado", new Exception("Erro Inner Exception"))
            };
            var exception = new ExceptionFilter();

            var result = await Record.ExceptionAsync(() => exception.OnExceptionAsync(exceptionContext));
            Assert.Null(result);
        }

        [Fact(DisplayName = "Exception Inner Exception")]
        public async Task OnExceptionFilterInnerException()
        {
            var actionContext = new ActionContext()
            {
                HttpContext = new DefaultHttpContext(),
                RouteData = new RouteData(),
                ActionDescriptor = new ActionDescriptor()
            };

            var exceptionContext = new ExceptionContext(actionContext, new List<IFilterMetadata>())
            {
                Exception = new Exception("Erro genérico", new Exception("Erro Inner Exception"))
            };
            var exception = new ExceptionFilter();

            var result = await Record.ExceptionAsync(() => exception.OnExceptionAsync(exceptionContext));
            Assert.Null(result);
        }

        [Fact(DisplayName = "InformacaoException null")]
        public async Task OnExceptionFilterInformacaoExceptionNull()
        {
            var actionContext = new ActionContext()
            {
                HttpContext = new DefaultHttpContext(),
                RouteData = new RouteData(),
                ActionDescriptor = new ActionDescriptor()
            };

            var exceptionContext = new ExceptionContext(actionContext, new List<IFilterMetadata>())
            {
                Exception = new InformacaoException(StatusException.NaoEncontrado, new List<string>())
            };
            var exception = new ExceptionFilter();

            var result = await Record.ExceptionAsync(() => exception.OnExceptionAsync(exceptionContext));
            Assert.Null(result);
        }

        [Fact(DisplayName = "Exception Null")]
        public async Task OnExceptionFilterNull()
        {
            var actionContext = new ActionContext()
            {
                HttpContext = new DefaultHttpContext(),
                RouteData = new RouteData(),
                ActionDescriptor = new ActionDescriptor()
            };

            var exceptionContext = new ExceptionContext(actionContext, new List<IFilterMetadata>())
            {
                Exception = null,
            };
            var exception = new ExceptionFilter();

            var result = await Record.ExceptionAsync(() => exception.OnExceptionAsync(exceptionContext));
            Assert.Null(result);
        }
    }
}
