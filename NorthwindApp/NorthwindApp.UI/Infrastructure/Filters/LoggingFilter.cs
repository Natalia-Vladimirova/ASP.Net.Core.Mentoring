using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NorthwindApp.UI.Infrastructure.Configuration;
using NorthwindApp.UI.Interfaces;

namespace NorthwindApp.UI.Infrastructure.Filters
{
    public class LoggingFilter : IActionFilter
    {
        private readonly LoggingOptions _loggingOptions;
        private readonly ILogger _logger;

        public LoggingFilter(IOptions<LoggingOptions> loggingOptions, ILogger logger)
        {
            _loggingOptions = loggingOptions.Value;
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var message = "Action starting";

            if (_loggingOptions.ShouldLogActionParameters)
            {
                _logger.LogInfo(message, GetDictionaryWithSerializedValues(context.ActionArguments));
            }
            else
            {
                _logger.LogInfo(message);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInfo("Action finished");
        }

        private IDictionary<string, string> GetDictionaryWithSerializedValues(IDictionary<string, object> parameters)
        {
            return parameters.ToDictionary(x => x.Key, x => JsonConvert.SerializeObject(x.Value));
        }
    }
}
