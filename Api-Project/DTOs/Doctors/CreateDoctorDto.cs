using Api_Project.Models;
using System.ComponentModel.DataAnnotations;

namespace Api_Project.DTOs.Doctors
{
    public class CreateDoctorDto
    {
        [Required(ErrorMessage = "Name Required")]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public int SpecialtyId { get; set; }

        [Required]
        public int LocationId { get; set; }

        [Range(0, 50)]
        public int YearsOfExperience { get; set; }

        [Range(1, double.MaxValue)]
        public decimal ConsultationFee { get; set; }

        public string? ProfileImage { get; set; }

        [MaxLength(1000)]
        public string? Bio { get; set; }
    }
}
