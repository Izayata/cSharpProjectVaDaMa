using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beadando.Contract
{
    public class Job
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id;

        [Required]
        public string ClientName;

        [Required]
        public string CarType;

        [Required]
        public string LicensePlateNumber;

        [Required]
        public int ManufacturingYear;

        [Required]
        public string Description;

        [Required]
        public JobCategory Category;

        [Required]
        public double ManHourEstimation;

        [Required]
        public JobStatus Status;
    }
}