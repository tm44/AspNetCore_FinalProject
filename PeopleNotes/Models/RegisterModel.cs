using System.ComponentModel.DataAnnotations;

namespace PeopleNotes.Models
{
    public class RegisterModel
    {
        [Required]
        [Display(Name = "User name")]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "Passwords must match")]
        public string ConfirmPassword { get; set; }
    }
}