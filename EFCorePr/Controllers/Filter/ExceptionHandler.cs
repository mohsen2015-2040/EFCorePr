using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EFCorePr.Controllers.Filter
{
    public class ExceptionHandler : IActionFilter
    {
        private readonly ILogger _logger;

        public ExceptionHandler(ILogger<ExceptionHandler> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if(context.Exception != null) 
            {
                var ex = context.Exception;
               
                _logger.LogError(ex.Message);

                context.Result = new ObjectResult(ex.Message);

                context.ExceptionHandled = true;
            }
        }

        public void OnActionExecuting(ActionExecutingContext context) { }
    }
}
