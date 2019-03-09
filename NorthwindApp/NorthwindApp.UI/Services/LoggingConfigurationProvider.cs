using Microsoft.Extensions.Configuration;
using NorthwindApp.UI.Interfaces;
using System.Collections.Generic;
using ConfigurationProvider = NorthwindApp.Core.Providers.ConfigurationProvider;

namespace NorthwindApp.UI.Services
{
    public class LoggingConfigurationProvider : ConfigurationProvider
    {
        private readonly ILogger _logger;

        public LoggingConfigurationProvider(IConfiguration configuration, ILogger logger)
            : base(configuration)
        {
            _logger = logger;
        }

        public override int ProductPageSize
        {
            get
            {
                var productPageSize = base.ProductPageSize;
                var properties = new Dictionary<string, string>
                {
                    { "ProductPageSize", productPageSize.ToString() }
                };
                _logger.LogInfo("Reading configuration", properties);

                return productPageSize;
            }
        }
    }
}
