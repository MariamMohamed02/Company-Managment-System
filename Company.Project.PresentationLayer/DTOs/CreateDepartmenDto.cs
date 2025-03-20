using System.ComponentModel.DataAnnotations;

namespace Company.Project.PresentationLayer.DTOs
{
    public class CreateDepartmenDto
    {
        [Required(ErrorMessage ="Code is required")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "CreatedAt is required")]
        public DateTime CreatedAt { get; set; }
    }
}
