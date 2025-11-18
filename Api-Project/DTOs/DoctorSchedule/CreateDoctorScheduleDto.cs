using System.ComponentModel.DataAnnotations;

namespace Api_Project.DTOs.DoctorSchedule
{
    public class CreateDoctorScheduleDto
    {
        [Required]
        public int DoctorId { get; set; }

        [Required]
        public DayOfWeek DayOfWeek { get; set; }

        [Required]
        public TimeSpan StartTime { get; set; }

        [Required]
        public TimeSpan EndTime { get; set; }

        public int SlotDurationMinutes { get; set; } = 30;

        public bool IsAvailable { get; set; } = true;
    }
}
