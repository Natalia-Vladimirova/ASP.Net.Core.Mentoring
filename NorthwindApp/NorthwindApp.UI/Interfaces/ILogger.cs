using System.Collections.Generic;

namespace NorthwindApp.UI.Interfaces
{
    public interface ILogger
    {
        void LogInfo(string info, IDictionary<string, string> properties);
    }
}
