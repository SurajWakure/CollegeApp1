using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Collegeapp1.Data
{
    public class StudentDTO
    {
        [ValidateNever]
        public int Id { get; set; }
        [Required(ErrorMessage = "student name is required")]
        public string Name { get; set; }
        [EmailAddress(ErrorMessage = "enter correct emailaddress")]
        [Remote(action: "VarifyEmail", controller: "Student")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Enter correct address")]
        public string Address { get; set; }
        public DateTime DOB { get; set; }
        public int marks { get; set; }
    }
}
