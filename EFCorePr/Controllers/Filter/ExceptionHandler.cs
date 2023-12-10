using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EFCorePr.Controllers.Filter
{
    public class ExceptionHandler : IExceptionFilter
    {
        private readonly ILogger _logger;
        private readonly IHostEnvironment _enviroment;

        public ExceptionHandler(IHostEnvironment environment, ILogger<ExceptionHandler> logger)
        {
            _logger = logger;
            _enviroment = environment;
        }


        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnException(ExceptionContext context)
        {
            if (!_enviroment.IsDevelopment()) { return; }

            context.Result = new ContentResult
            {
                Content = context.Exception.Message
            };

            _logger.LogError(context.Exception.ToString());
        }
    }
}
