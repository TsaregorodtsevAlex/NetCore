using System;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Microsoft.AspNetCore.Mvc.Filters;

namespace NetCoreLoger
{
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
	public class LogActionAttribute : ActionFilterAttribute
	{
		private readonly string _actionDescription;

		public LogActionAttribute(string actionDescription)
		{
			_actionDescription = actionDescription;
		}

		public override void OnActionExecuting(ActionExecutingContext context)
		{
			Log.Information("Start executing {ActionDescription}. Controller {Controller}, Action {Action}.",
				_actionDescription,
				((ControllerBase)context.Controller).ControllerContext.ActionDescriptor.ControllerName,
				context.ActionDescriptor.DisplayName);
			base.OnActionExecuting(context);
		}

		public override void OnActionExecuted(ActionExecutedContext context)
		{
			Log.Information("End executing {ActionDescription}. Controller: {Controller}, Action: {Action}.",
				_actionDescription,
				((ControllerBase)context.Controller).ControllerContext.ActionDescriptor.ControllerName,
				context.ActionDescriptor.DisplayName);
			base.OnActionExecuted(context);
		}
	}
}