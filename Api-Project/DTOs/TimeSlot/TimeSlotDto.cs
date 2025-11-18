using System.ComponentModel.DataAnnotations;

namespace Api_Project.DTOs.TimeSlot
{
    public class TimeSlotDto
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public bool IsBooked { get; set; }
        public int? AppointmentId { get; set; }
    }
}
