using System.ComponentModel.DataAnnotations;

namespace UserService.Models
{
    public class LoginDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }

    }
}
