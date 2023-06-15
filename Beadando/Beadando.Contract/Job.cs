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
        [MaxLength(15)]
        public string ClientFirstName { get; set; }

        [Required]
        [MaxLength(15)]
        public string ClientLastName { get; set; }

        [Required]
        [MaxLength(20)]
        public string CarType { get; set; }

        [Required]
        public string LicensePlateNumber { get; set; }

        [Required]
        public int ManufacturingYear { get; set; }

        [Required]
        public JobCategory Category { get; set; }

        [Required]
        [MaxLength(100)]
        public string Description { get; set; }

        [Required]
        [Range(1, 10)]
        public int Severity { get; set; }

        public double ManHourEstimation { get; set; }

        public JobStatus Status { get; set; }
    }
}