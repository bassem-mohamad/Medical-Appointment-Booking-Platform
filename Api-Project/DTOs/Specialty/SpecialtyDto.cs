using System.ComponentModel.DataAnnotations;
using Api_Project.Models;

namespace Api_Project.DTOs.Specialty
{
    public class SpecialtyDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name Required")]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }
        //public List<Doctor> Doctors { get; set; }
    }
}
