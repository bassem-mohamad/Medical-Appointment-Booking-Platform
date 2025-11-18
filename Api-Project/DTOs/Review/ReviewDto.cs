using System.ComponentModel.DataAnnotations;

namespace Api_Project.DTOs.Review
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public int AppointmentId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
    }
}
