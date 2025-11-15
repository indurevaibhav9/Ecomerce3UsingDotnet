using System.ComponentModel.DataAnnotations;

namespace FrontendByDotnteMVC.Models.Customer
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string UserName { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }
        [Required]
        [Phone]
        public string Phone { get; set; }
    }
}
