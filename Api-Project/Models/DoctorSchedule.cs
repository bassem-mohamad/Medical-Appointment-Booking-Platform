using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api_Project.Models
{
    // 6. موديل جدول الدكتور
    public class DoctorSchedule : BaseEntity
    {
        [Required]
        public int DoctorId { get; set; }

        [ForeignKey(nameof(DoctorId))]
        public Doctor Doctor { get; set; }

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
