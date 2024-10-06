using System.ComponentModel.DataAnnotations;

namespace Collegeapp1.Data
{
    public class LoginDTO
    {
        [Required]
        public string Policy { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

    }
}
