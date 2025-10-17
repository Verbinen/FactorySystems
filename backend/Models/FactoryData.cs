namespace Models
{
    public sealed class FactoryData
    {
        public Guid Id { get; set; }
        public string ApplicationName { get; set; }
        public int ApplicationCode { get; set; }
        public string[] EmailSupport { get; set; }
        public string Status { get; set; }
        public string Database { get; set; }
        public string InstallationDirectory { get; set; }
    }
}
