using System.ComponentModel.DataAnnotations;

namespace CrudApp.DTOs
{
    public class CreateUserDTO
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Retype your password")]
        public string RePassword { get; set; }

        public string Phone { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; }

        public bool Status { get; set; }
    }
}
