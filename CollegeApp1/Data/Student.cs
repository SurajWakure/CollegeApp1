using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Collegeapp1.Data
{
    public class Student
    {
       
        public int Id { get; set; }
        
        public string Name { get; set; }
       
        public string Email { get; set; }
       
        public string Address { get; set; }
        public DateTime DOB { get; set; }
        public int marks { get; set; }

        public int? DepartmentId { get; set; }


        public virtual Department? Department { get; set; }
    }
}
