using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api_Project.Models
{
    public class Doctor : BaseEntity
    {
        [Required(ErrorMessage = "Name Required")]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public int SpecialtyId { get; set; }

        [ForeignKey(nameof(SpecialtyId))]
        public Specialty Specialty { get; set; }

        [Required]
        public int LocationId { get; set; }

        [ForeignKey(nameof(LocationId))]
        public Location Location { get; set; }

        [Range(0, 50)]
        public int YearsOfExperience { get; set; }

        [Range(1, double.MaxValue)]
        public decimal ConsultationFee { get; set; }

        public string? ProfileImage { get; set; }

        [MaxLength(1000)]
        public string Bio { get; set; }

        public List<Appointment> Appointments { get; set; }
        public List<DoctorSchedule> Schedules { get; set; }
        public List<Review> Reviews { get; set; }
    }
}
