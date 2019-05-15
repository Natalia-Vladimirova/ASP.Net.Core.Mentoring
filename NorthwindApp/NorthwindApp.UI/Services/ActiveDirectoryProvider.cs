using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using NorthwindApp.UI.Infrastructure.Configuration;
using NorthwindApp.UI.Interfaces;

namespace NorthwindApp.UI.Services
{
    public class ActiveDirectoryProvider : IActiveDirectoryProvider
    {
        private readonly AzureAdGroup[] _groupMap;

        public ActiveDirectoryProvider(IOptions<AzureAdGroupConfig> groupConfig)
        {
            _groupMap = groupConfig.Value.GroupMap;
        }

        public IEnumerable<string> GetGroupNames(IEnumerable<string> groupIds)
        {
            var groupIdsArray = groupIds?.ToArray();

            if (groupIdsArray == null || groupIdsArray.Length == 0)
            {
                return Enumerable.Empty<string>();
            }

            return _groupMap
                .Where(x => groupIdsArray.Any(y => string.Equals(y, x.Id, StringComparison.OrdinalIgnoreCase)))
                .Select(x => x.Name);
        }
    }
}
