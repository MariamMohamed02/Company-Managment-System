using System.ComponentModel.DataAnnotations;

namespace Company.Project.PresentationLayer.DTOs
{
    public class SignInDto
    {
        [Required(ErrorMessage = "Password is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

       

        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress]
        public string Email { get; set; }
        public bool RememberMe { get; set; }
    }
}
