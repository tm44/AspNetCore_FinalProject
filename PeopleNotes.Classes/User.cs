using System.ComponentModel.DataAnnotations;

namespace PeopleNotes.Classes
{
    public class User
    {
        public int UserId { get; set; }
        [Display(Name = "User name")]
        public string Username { get; set; }
        public string Password { get; set; }
        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
        public List<Person>? People { get; set; }
    }
}
