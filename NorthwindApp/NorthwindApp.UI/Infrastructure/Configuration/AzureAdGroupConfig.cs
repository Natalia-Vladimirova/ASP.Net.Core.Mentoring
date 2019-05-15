namespace NorthwindApp.UI.Infrastructure.Configuration
{
    public class AzureAdGroupConfig
    {
        public AzureAdGroup[] GroupMap { get; set; }
    }

    public class AzureAdGroup
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
