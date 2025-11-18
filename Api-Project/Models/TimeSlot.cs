using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api_Project.Models
{
    public class TimeSlot : BaseEntity
    {
        [Required]
        public int DoctorId { get; set; }

        [ForeignKey(nameof(DoctorId))]
        public Doctor Doctor { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public TimeSpan Time { get; set; }

        public bool IsBooked { get; set; } = false;

        public int? AppointmentId { get; set; }
    }
}
