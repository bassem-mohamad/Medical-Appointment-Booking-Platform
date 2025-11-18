using System.ComponentModel.DataAnnotations;

namespace Api_Project.DTOs.Appointment
{
    public class CreateAppointmentDto
    {
        [Required]
        public int DoctorId { get; set; }

        [Required]
        public DateTime AppointmentDate { get; set; }

        [Required]
        public TimeSpan AppointmentTime { get; set; }

        public string Notes { get; set; }
    }
}
