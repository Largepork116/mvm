using DavidMorales.Domain.Exceptions;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

using System;
using System.Collections.Generic;
using System.Linq;

namespace DavidMorales.Services.Api.Helpers
{
    public static class ResponseHelper
    {
        #region OK Response

        public static OkObjectResult Ok(object data)
        {
            return new OkObjectResult(new
            {
                Data = data,
                Success = true
            });
        }

        public static OkObjectResult Ok(string message)
        {
            return new OkObjectResult(new
            {
                Message = message,
                Success = true
            });
        }

        #endregion

        #region BadRequest Response

        public static BadRequestObjectResult BadRequest(Exception ex)
        {
            var result = new List<ErrorObjectResult>
            {
                new ErrorObjectResult("Unknown", ex.Message)
            };

            return new BadRequestObjectResult(new
            {
                Errors = result,
                Success = false
            });
        }

        public static BadRequestObjectResult BadRequest(AppException ex)
        {
            var result = new List<ErrorObjectResult>
            {
                new ErrorObjectResult("Business", ex.Message)
            };

            return new BadRequestObjectResult(new
            {
                Errors = result,
                Success = false
            });
        }

        public static BadRequestObjectResult BadRequest(ModelStateDictionary modelState)
        {
            var result = new List<ErrorObjectResult>();

            var erroneousFields = modelState.Where(ms => ms.Value.Errors.Any())
                                    .Select(x => new { x.Key, x.Value.Errors });

            foreach (var erroneousField in erroneousFields)
            {
                var fieldKey = erroneousField.Key;
                var fieldErrors = erroneousField.Errors
                                   .Select(error =>
                                   {
                                       if (error.ErrorMessage.Trim().Length > 0)
                                           return new ErrorObjectResult("FieldValidations", $"{fieldKey}: {error.ErrorMessage}");
                                       else
                                           return new ErrorObjectResult("FieldValidations", $"{fieldKey}: {error.Exception.Message}");
                                   });
                result.AddRange(fieldErrors);
            }

            return new BadRequestObjectResult(new
            {
                Errors = result,
                Success = false
            });
        }

        public static BadRequestObjectResult BadRequest(IdentityResult identityResult)
        {
            var result = new List<ErrorObjectResult>();

            foreach (var e in identityResult.Errors)
            {
                var error = new ErrorObjectResult("IdentityValidations", $"{e.Code}: {e.Description}");
                result.Add(error);
            }

            return new BadRequestObjectResult(new
            {
                Errors = result,
                Success = false
            });
        }

        public static BadRequestObjectResult BadRequest(AppAuthException ex)
        {
            var result = new List<ErrorObjectResult>
            {
                new ErrorObjectResult("Auth", ex.Message)
            };

            return new BadRequestObjectResult(new
            {
                Errors = result,
                Success = false
            });
        }

        #endregion

        #region NotFound Response

        public static NotFoundObjectResult NotFound(AppNotFoundException ex)
        {
            var result = new List<ErrorObjectResult>
            {
                new ErrorObjectResult("NotFound", ex.Message)
            };

            return new NotFoundObjectResult(new
            {
                Errors = result,
                Success = false
            });
        }

        #endregion
    }

    public class ErrorObjectResult
    {
        public ErrorObjectResult(string key, string message)
        {
            Key = key;
            Message = message;
        }

        public string Key { get; set; }
        public string Message { get; set; }
    }
}
