using System.ComponentModel.DataAnnotations;
using Api_Project.Models;

namespace Api_Project.DTOs.Location
{
    public class LocationDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "City required")]
        [MaxLength(100)]
        public string City { get; set; }

        [MaxLength(100)]
        public string Area { get; set; }
        //public List<Doctor> Doctors { get; set; }
    }
}
