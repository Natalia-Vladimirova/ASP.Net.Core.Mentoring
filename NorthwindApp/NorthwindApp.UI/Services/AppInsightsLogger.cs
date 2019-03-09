using System.Collections.Generic;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using NorthwindApp.UI.Interfaces;

namespace NorthwindApp.UI.Services
{
    public class AppInsightsLogger : ILogger
    {
        private readonly TelemetryClient _client;

        public AppInsightsLogger()
        {
            _client = new TelemetryClient();
        }

        public void LogInfo(string info, IDictionary<string, string> properties)
        {
            _client.TrackTrace(info, SeverityLevel.Information, properties);
        }
    }
}
