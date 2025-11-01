using System.ComponentModel.DataAnnotations;

namespace Api_Project.Models
{
    public class Specialty : BaseEntity
    {

        [Required(ErrorMessage ="Name Required")]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        //public string Icon { get; set; }
        public List<Doctor> Doctors { get; set; }
    }
}
