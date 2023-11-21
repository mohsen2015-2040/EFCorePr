using Microsoft.AspNetCore.Mvc.Filters;

namespace EFCorePr.Controllers.Filter
{
    public class LogActionActivity : IAsyncActionFilter
    {
        private readonly ILogger<LogActionActivity> _logger;

        public LogActionActivity(ILogger<LogActionActivity> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context) { }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            _logger.LogInformation($"{context.ActionDescriptor.DisplayName} executed.");

            await _logger.LogToFile(@$"..\EFCorePr\bin\saves\{context.ActionDescriptor.DisplayName}", $"{context.ActionDescriptor.DisplayName} executed.");

            await next();
        }
    }
}
