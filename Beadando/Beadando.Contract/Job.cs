using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beadando.Contract
{
    public class Job
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string ClientName { get; set; }

        [Required]
        public string CarType { get; set; }

        [Required]
        public string LicensePlateNumber { get; set; }

        [Required]
        public int ManufacturingYear { get; set; }

        [Required]
        public JobCategory Category { get; set; }

        [Required]
        public int Severity { get; set; }

        public string Description { get; set; }

        public double ManHourEstimation { get; set; }

        public JobStatus Status { get; set; }
    }
}