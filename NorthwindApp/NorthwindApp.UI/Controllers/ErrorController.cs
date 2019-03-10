using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NorthwindApp.UI.Interfaces;

namespace NorthwindApp.UI.Controllers
{
    [AllowAnonymous]
    public class ErrorController : Controller
    {
        private readonly ILogger _logger;

        public ErrorController(ILogger logger)
        {
            _logger = logger;
        }

        [Route("Error/500")]
        public IActionResult InternalError()
        {
            LogException(500);

            return View();
        }

        [Route("Error/404")]
        public IActionResult NotFoundError()
        {
            LogReExecutedError(404);

            return View();
        }

        [Route("Error/{errorCode:int}")]
        public IActionResult DefaultError(int errorCode)
        {
            LogReExecutedError(errorCode);

            return View(errorCode);
        }

        private void LogException(int statusCode)
        {
            var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            if (exceptionHandlerPathFeature == null)
            {
                return;
            }

            var properties = new Dictionary<string, string>
            {
                ["StatusCode"] = statusCode.ToString(),
                ["OriginalPath"] = exceptionHandlerPathFeature.Path
            };

            _logger.LogError(exceptionHandlerPathFeature.Error, properties);
        }

        private void LogReExecutedError(int statusCode)
        {
            var statusCodeReExecuteFeature = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

            if (statusCodeReExecuteFeature == null)
            {
                return;
            }

            var properties = new Dictionary<string, string>
            {
                ["StatusCode"] = statusCode.ToString(),
                ["OriginalPath"] = statusCodeReExecuteFeature.OriginalPath,
                ["OriginalQueryString"] = statusCodeReExecuteFeature.OriginalQueryString
            };

            _logger.LogError($"{statusCode} - Error", properties);
        }
    }
}
