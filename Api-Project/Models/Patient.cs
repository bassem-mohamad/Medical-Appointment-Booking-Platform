using System.ComponentModel.DataAnnotations;

namespace Api_Project.Models
{
    public enum Gender
    {
        Male = 1,
        Female = 2,
    }
    // 5. موديل المرضى
    public class Patient : BaseEntity
    {
        [Required(ErrorMessage ="Full Name Required")]
        [StringLength(100)]
        public string FullName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public Gender Gender { get; set; }
        public List<Appointment> Appointments { get; set; }
        public List<Review> Reviews { get; set; }
    }
}
