using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beadando.Contract
{
    public class Job
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Client First Name Required!")]
        [RegularExpression(@"[^a-zA-Z0-9]", ErrorMessage = "Invalid Client First Name Format!")]
        [MaxLength(15)]
        public string ClientFirstName { get; set; }

        [Required(ErrorMessage = "Client Last Name Required!")]
        [MaxLength(15)]
        [RegularExpression(@"[^a-zA-Z0-9]", ErrorMessage = "Invalid Client Last Name Format!")]
        public string ClientLastName { get; set; }

        [Required(ErrorMessage = "Car Type Required!")]
        [MaxLength(20)]
        [RegularExpression(@"[^a-zA-Z0-9]", ErrorMessage = "Invalid Car Type Format!")]
        public string CarType { get; set; }

        [Required(ErrorMessage = "License Plate Number Required!")]
        [RegularExpression(@"[A-Z]{3}-[0-9]{3}", ErrorMessage = "Invalid Client Last Name Format!")]
        public string LicensePlateNumber { get; set; }

        [Required(ErrorMessage = "Manufacturing Year Required!")]
        public int ManufacturingYear { get; set; }

        [Required(ErrorMessage = "Job Category Required!")]
        public JobCategory Category { get; set; }

        [Required(ErrorMessage = "Job Description Required!")]
        [MaxLength(100)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Severity Required!")]
        [Range(1, 10, ErrorMessage = "Severity must be in the range of 1 and 10")]
        public int Severity { get; set; }

        public double ManHourEstimation { get; set; }

        public JobStatus Status { get; set; }
    }
}