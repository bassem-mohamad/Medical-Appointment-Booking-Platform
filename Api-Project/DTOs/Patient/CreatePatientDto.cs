using Api_Project.Models;
using System.ComponentModel.DataAnnotations;

namespace Api_Project.DTOs.Patient
{
    public class CreatePatientDto
    {
        [Required(ErrorMessage = "Full Name Required")]
        [StringLength(100)]
        public string FullName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public Gender Gender { get; set; }
    }
}
