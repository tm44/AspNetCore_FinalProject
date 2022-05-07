using System.ComponentModel.DataAnnotations;

namespace PeopleNotes.Classes
{
    public class Person
    {
        public int PersonId { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public List<Note>? Notes { get; set; }

        public int UserId { get; set; }
        
        [Required]
        public User User { get; set; }
    }
}
