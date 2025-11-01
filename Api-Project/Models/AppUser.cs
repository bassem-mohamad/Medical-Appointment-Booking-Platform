using System.ComponentModel.DataAnnotations;
using System.Numerics;
using System.Reflection;
using Microsoft.AspNetCore.Identity;

namespace Api_Project.Models
{
    public enum UserType
    {
        Patient = 1,
        Doctor = 2,
    }
    public class AppUser : IdentityUser
    {
        [MaxLength(500)]
        public string Address { get; set; }

        [Required(ErrorMessage = "Must be Select Type")]
        public UserType UserType { get; set; }

        public Patient? Patient { get; set; }
        public Doctor? Doctor { get; set; }
    }
}
