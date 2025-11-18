using System.ComponentModel.DataAnnotations;

namespace Api_Project.DTOs.Review
{
    public class CreateReviewDto
    {
        [Required]
        public int DoctorId { get; set; }

        [Required]
        public int AppointmentId { get; set; }

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        [StringLength(1000)]
        public string? Comment { get; set; }
    }
}
