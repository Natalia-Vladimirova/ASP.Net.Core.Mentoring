using Microsoft.AspNetCore.Mvc.Filters;
using NorthwindApp.Core.Interfaces;
using NorthwindApp.UI.Interfaces;
using System.Collections.Generic;

namespace NorthwindApp.UI.Infrastructure.Filters
{
    public class LoggingFilter : IActionFilter
    {
        private readonly IConfigurationProvider _configurationProvider;
        private readonly ILogger _logger;

        public LoggingFilter(IConfigurationProvider configurationProvider, ILogger logger)
        {
            _configurationProvider = configurationProvider;
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (_configurationProvider.LogActionMethodCalls)
            {
                _logger.LogInfo("Action starting", context.ActionDescriptor.RouteValues);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (_configurationProvider.LogActionMethodCalls)
            {
                _logger.LogInfo("Action finished", context.ActionDescriptor.RouteValues);
            }
        }
    }
}
