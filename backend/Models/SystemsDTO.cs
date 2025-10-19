using System.ComponentModel.DataAnnotations;

namespace Models
{
    public sealed class SystemsDTO
    {
        [Key]
        public Guid Id { get; set; }
        public string ApplicationName { get; set; }
        public string ApplicationCode { get; set; }
        public string CostCenter { get; set; }
        public string[] EmailSupport { get; set; }
        public string Status { get; set; }
        public string Database { get; set; }
        public string InstallationLocation { get; set; }
    }
}
