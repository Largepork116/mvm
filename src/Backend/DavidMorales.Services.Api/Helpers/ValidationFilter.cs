using Microsoft.AspNetCore.Mvc.Filters;

using System.Threading.Tasks;

namespace DavidMorales.Services.Api.Helpers
{
    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = ResponseHelper.BadRequest(context.ModelState);
                return;
            }

            await next();
        }
    }
}
