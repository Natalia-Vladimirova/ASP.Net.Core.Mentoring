using System.Collections.Generic;

namespace NorthwindApp.UI.Interfaces
{
    public interface IActiveDirectoryProvider
    {
        IEnumerable<string> GetGroupNames(IEnumerable<string> groupIds);
    }
}
