﻿namespace NorthwindApp.UI.Infrastructure.Configuration
{
    public class AzureAdOptions
    {
        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public string Instance { get; set; }

        public string TenantId { get; set; }

        public string CallbackPath { get; set; }
    }
}
