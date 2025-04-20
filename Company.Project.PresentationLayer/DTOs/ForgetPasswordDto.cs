using System.ComponentModel.DataAnnotations;

namespace Company.Project.PresentationLayer.DTOs
{
    public class ForgetPasswordDto
    {
        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress]
        public string Email { get; set; }
    }

}
