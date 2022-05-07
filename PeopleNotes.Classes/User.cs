namespace PeopleNotes.Classes
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }

        public List<Person>? People { get; set; }
    }
}
