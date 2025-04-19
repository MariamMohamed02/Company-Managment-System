//using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using Xunit;
using Xunit.Sdk;

namespace Company.Project.PresentationLayer.DTOs
{
    public class SignUpDto
    {
        [Required(ErrorMessage = "User Name is Required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "First Name is Required")]
        public string FisrtName { get; set; }

        [Required(ErrorMessage = "LAst Name is Required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is Required")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage="Confirm Password Doesn't Match the Passsword")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress]
        public string Email { get; set; }
        public bool IsAgree { get; set; }

    }
}
