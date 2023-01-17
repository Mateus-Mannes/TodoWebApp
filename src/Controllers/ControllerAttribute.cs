using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TodoApp;

namespace TodoApp.Controllers;

[AttributeUsage(validOn: AttributeTargets.Class | AttributeTargets.Method)]
public class ControllerAtribute : Attribute, IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next)
    {
        try
        {
            await next();
        }
        catch
        {
            context.Result = new ContentResult()
            {
                StatusCode = 500,
                Content = "Internal server error",
            };
        }
        
    }
}