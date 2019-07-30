using Serilog;
using Microsoft.AspNetCore.Mvc.Filters;

namespace NetCoreLog
{
    public class LogActionAttribute: ActionFilterAttribute
    {
        private readonly string _actionDescription;

        public LogActionAttribute(string actionDescription)
        {
            _actionDescription = actionDescription;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Log.Information($"Start executing {_actionDescription}");
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            Log.Information($"End executing {_actionDescription}");
        }
    }
}