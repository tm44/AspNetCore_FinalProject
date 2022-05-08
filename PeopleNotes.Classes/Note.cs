namespace PeopleNotes.Classes
{
    public class Note
    {
        public int NoteId { get; set; }
        public string Text { get; set; }
        public DateTime LastUpdated { get; set; }

        public int PersonId { get; set; }
        public Person? Person { get; set; }
    }
}
