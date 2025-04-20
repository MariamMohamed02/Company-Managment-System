using System.ComponentModel.DataAnnotations;

namespace Company.Project.PresentationLayer.DTOs
{
    public class ResetPasswordDto
    {
        [Required(ErrorMessage = "Password is Required")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm Password is Required")]
        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword), ErrorMessage = "Confirm Password Doesn't Match the Passsword")]
        public string ConfirmPassword { get; set; }

    }
}
