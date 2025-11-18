using System.ComponentModel.DataAnnotations;
using Api_Project.Models;

namespace Api_Project.DTOs.AppUser
{
    public class RegisterDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
        public string Address { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        //public UserType UserType { get; set; }
    }

}
