using System.ComponentModel.DataAnnotations;

namespace Api_Project.Models
{
    public class Location : BaseEntity
    {

        [Required(ErrorMessage ="City required")]
        [MaxLength(100)]
        public string City { get; set; }

        [MaxLength(100)]
        public string Area { get; set; }
        public List<Doctor> Doctors { get; set; }
    }
}
