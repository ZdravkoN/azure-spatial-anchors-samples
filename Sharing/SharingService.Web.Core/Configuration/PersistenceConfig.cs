namespace SharingService.Web.Core.Configuration
{
    public enum PersistenceProvider
    {
        None,
        InMemory,
        Sqlite,
        SqlServer,
        Cosmos
    }

    public class PersistenceConfig
    {
        public PersistenceProvider Provider { get; set; } = PersistenceProvider.None;
        public string ConnectionString { get; set; }
        public string AccessKey { get; set; }
        public string DatabaseName { get; set; }
    }
}
