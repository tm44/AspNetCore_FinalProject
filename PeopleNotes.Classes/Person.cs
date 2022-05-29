using System.ComponentModel.DataAnnotations;

namespace PeopleNotes.Classes
{
    public class Person
    {
        public int PersonId { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string? LastName { get; set; }
        public List<Note>? Notes { get; set; }

        public int UserId { get; set; }
        
        public User? User { get; set; }
    }
}
