using DavidMorales.Domain.Exceptions;

using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

using System;

namespace DavidMorales.Services.Api.Helpers
{
    public class HttpResponseExceptionFilter : IExceptionFilter
    {
        private readonly ILogger _logger;

        public HttpResponseExceptionFilter(ILogger<HttpResponseExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            if (context.Exception is AppNotFoundException)
            {
                context.Result = ResponseHelper.NotFound(context.Exception as AppNotFoundException);
                context.ExceptionHandled = true;
            }

            else if (context.Exception is AppException)
            {
                context.Result = ResponseHelper.BadRequest(context.Exception as AppException);
                context.ExceptionHandled = true;
            }

            else if (context.Exception is AppAuthException)
            {
                context.Result = ResponseHelper.BadRequest(context.Exception as AppAuthException);
                _logger.LogError(context.Exception, "An Auth error occurred");
                context.ExceptionHandled = true;
            }

            else if (context.Exception is Exception)
            {
                context.Result = ResponseHelper.BadRequest(context.Exception);
                _logger.LogError(context.Exception, "An unhandled error occurred");
                context.ExceptionHandled = true;
            }
        }
    }
}