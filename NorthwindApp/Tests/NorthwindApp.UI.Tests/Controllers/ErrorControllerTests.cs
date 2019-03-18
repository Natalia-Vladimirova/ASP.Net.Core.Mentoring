using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NorthwindApp.UI.Controllers;
using NorthwindApp.UI.Interfaces;
using Xunit;

namespace NorthwindApp.UI.Tests.Controllers
{
    public class ErrorControllerTests
    {
        private readonly ErrorController _errorController;

        private readonly Mock<ILogger> _loggerMock;

        private const string ErrorMessage = "NorthwindApp UI Exception";
        private const string ExceptionPath = "Exception Path";
        private const string ReExecuteFeaturePath = "ReExecute Path";
        private const string ReExecuteFeatureQueryString = "Query String";

        public ErrorControllerTests()
        {
            _loggerMock = new Mock<ILogger>();

            var exceptionHandlerPathFeatureMock = new Mock<IExceptionHandlerPathFeature>();
            exceptionHandlerPathFeatureMock.Setup(x => x.Error).Returns(new Exception(ErrorMessage));
            exceptionHandlerPathFeatureMock.Setup(x => x.Path).Returns(ExceptionPath);

            var statusCodeReExecuteFeatureMock = new Mock<IStatusCodeReExecuteFeature>();
            statusCodeReExecuteFeatureMock.Setup(x => x.OriginalPath).Returns(ReExecuteFeaturePath);
            statusCodeReExecuteFeatureMock.Setup(x => x.OriginalQueryString).Returns(ReExecuteFeatureQueryString);

            var controllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };
            controllerContext.HttpContext.Features.Set(exceptionHandlerPathFeatureMock.Object);
            controllerContext.HttpContext.Features.Set(statusCodeReExecuteFeatureMock.Object);

            _errorController = new ErrorController(_loggerMock.Object)
            {
                ControllerContext = controllerContext
            };
        }

        [Fact]
        public void Test_InternalError_ShouldLogError()
        {
            _errorController.InternalError();

            _loggerMock.Verify(x => x.LogError(
                It.Is<Exception>(y => y.Message == ErrorMessage),
                It.Is<IDictionary<string, string>>(y => VerifyInternalErrorProperties(y))), Times.Once);
        }

        [Fact]
        public void Test_NotFoundError_ShouldLogError()
        {
            _errorController.NotFoundError();

            _loggerMock.Verify(x => x.LogError(
                It.IsAny<string>(),
                It.Is<IDictionary<string, string>>(y => VerifyReExecuteFeatureProperties(y, "404"))), Times.Once);
        }

        [Fact]
        public void Test_DefaultError_ShouldLogError()
        {
            _errorController.DefaultError(429);

            _loggerMock.Verify(x => x.LogError(
                It.IsAny<string>(),
                It.Is<IDictionary<string, string>>(y => VerifyReExecuteFeatureProperties(y, "429"))), Times.Once);
        }

        private bool VerifyInternalErrorProperties(IDictionary<string, string> properties)
        {
            return properties.ContainsKey("StatusCode") && properties["StatusCode"] == "500" &&
                properties.ContainsKey("OriginalPath") && properties["OriginalPath"] == ExceptionPath;
        }

        private bool VerifyReExecuteFeatureProperties(IDictionary<string, string> properties, string statusCode)
        {
            return properties.ContainsKey("StatusCode") && properties["StatusCode"] == statusCode &&
                properties.ContainsKey("OriginalPath") && properties["OriginalPath"] == ReExecuteFeaturePath &&
                properties.ContainsKey("OriginalQueryString") && properties["OriginalQueryString"] == ReExecuteFeatureQueryString;
        }
    }
}
