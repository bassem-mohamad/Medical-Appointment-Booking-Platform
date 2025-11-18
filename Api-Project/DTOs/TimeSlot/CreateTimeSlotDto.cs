using System.ComponentModel.DataAnnotations;

namespace Api_Project.DTOs.TimeSlot
{
    public class CreateTimeSlotDto
    {
        [Required]
        public int DoctorId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public TimeSpan Time { get; set; }
    }
}
