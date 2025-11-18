using Api_Project.Models;

namespace Api_Project.DTOs.Appointment
{
    public class AppointmentDto
    {
        public int Id { get; set; }
        public string DoctorName { get; set; }
        public string PatientName { get; set; }
        public DateTime AppointmentDate { get; set; }
        public TimeSpan AppointmentTime { get; set; }
        public AppointmentStatus Status { get; set; }
        public string Notes { get; set; }
        public decimal Fee { get; set; }
        public DateTime BookedAt { get; set; }
    }
}
