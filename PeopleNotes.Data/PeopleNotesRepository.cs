using PeopleNotes.Classes;

namespace PeopleNotes.Data
{
    public class PeopleNotesRepository : IPeopleNotesRepository
    {
        private readonly PeopleNotesContext _context;

        public PeopleNotesRepository()
        {
            _context = new PeopleNotesContext();
        }

        public IEnumerable<Person> GetPeopleByName(int userId, string name)
        {
            return _context.People.AsEnumerable<Person>().Where(p => 
                p.UserId == userId &&
                isNameMatch(p, name));
        }

        public IEnumerable<Note> GetNotesForPerson(int personId)
        {
            var notes = _context.Notes.Where(n => n.PersonId == personId).ToList();
            notes.ForEach(n => n.PersonId = personId);
            return notes;
        }

        public IEnumerable<Person> GetPeople(int userId)
        {
            return _context.People.Where(p => p.UserId == userId);
        }

        private bool isNameMatch(Person person, string search)
        {
            return person.FirstName.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                (!string.IsNullOrEmpty(person.LastName) && person.LastName.Contains(search, StringComparison.OrdinalIgnoreCase));
        }

        public Person GetPersonById(int userId, int id)
        {
            return _context.People.First(p => p.PersonId == id);
        }

        public Note CreateNote(Note note)
        {
            var savedNote = _context.Notes.Add(note);
            _context.SaveChanges();
            return note;
        }
    }
}
