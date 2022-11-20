using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NetModule2_1.BAL;

namespace NetModule2_1.API
{
    public class ErrorHandlingAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is CartNotFoundException)
                context.Result = new NotFoundResult();
            if (context.Exception is InvalidItemException)
                context.Result = new BadRequestObjectResult(context.Exception.Message);
        }
    }
}
