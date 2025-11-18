using Api_Project.Models;
using System.ComponentModel.DataAnnotations;

namespace Api_Project.DTOs.Appointment
{
    public class UpdateAppointmentDto
    {
        [Required]
        public AppointmentStatus Status { get; set; }

        public string? Notes { get; set; }

        public string? CancellationReason { get; set; }
    }
}
