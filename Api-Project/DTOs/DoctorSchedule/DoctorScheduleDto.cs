using System.ComponentModel.DataAnnotations;

namespace Api_Project.DTOs.DoctorSchedule
{
    public class DoctorScheduleDto
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int SlotDurationMinutes { get; set; }
        public bool IsAvailable { get; set; }
    }
}
