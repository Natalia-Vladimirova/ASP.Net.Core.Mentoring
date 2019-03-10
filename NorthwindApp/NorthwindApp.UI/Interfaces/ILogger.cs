using System;
using System.Collections.Generic;

namespace NorthwindApp.UI.Interfaces
{
    public interface ILogger
    {
        void LogInfo(string info, IDictionary<string, string> properties);

        void LogError(Exception exception, IDictionary<string, string> properties);

        void LogError(string message, IDictionary<string, string> properties);
    }
}
