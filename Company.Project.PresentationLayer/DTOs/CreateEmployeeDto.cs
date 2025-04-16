//using Microsoft.Build.Framework;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Company.Project.DataLayer.Models;

namespace Company.Project.PresentationLayer.DTOs
{
    public class CreateEmployeeDto
    {
       

        [Required(ErrorMessage="Name is Required!")]
        public string Name { get; set; }

        [Range(22,69, ErrorMessage =" Age Must Be between 22 and 69")]
        public int? Age { get; set; }

        [DataType(DataType.EmailAddress , ErrorMessage ="Email is Not Valid!")]
        public string Email { get; set; }

        [RegularExpression(pattern: @"[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$",
         ErrorMessage = "Address must be like 123-street-city-country")]
        public string Address { get; set; }

        [Phone]
        public string Phone { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        [DisplayName("Hiring Date")]
        public DateTime HiringDate { get; set; }

        [DisplayName("Date of Creation")]
        public DateTime CreateAt { get; set; }
        public int? DepartmentId { get; set; }


    }
}
