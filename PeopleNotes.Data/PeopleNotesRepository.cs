using PeopleNotes.Classes;
using Microsoft.EntityFrameworkCore;

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
            return _context.People.Include(p => p.Notes).Where(p => p.UserId == userId);
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
            note.LastUpdated = DateTime.Now;
            var savedNote = _context.Notes.Add(note);
            _context.SaveChanges();
            return note;
        }

        public Note GetNoteById(int noteId)
        {
            return _context.Notes.First(n => n.NoteId == noteId);
        }

        public Note UpdateNote(Note note)
        {
            var updatedNote = _context.Notes.FirstOrDefault(n => n.NoteId == note.NoteId);
            if (updatedNote != null)
            {
                updatedNote.Text = note.Text;
                updatedNote.LastUpdated = DateTime.Now;
                _context.SaveChanges();
            }
            return updatedNote;
        }

        public void DeleteNote(int noteId)
        {
            var note = _context.Notes.FirstOrDefault(n => n.NoteId == noteId);
            if (note != null)
            {
                _context.Notes.Remove(note);
                _context.SaveChanges();
            }
        }

        public User GetUser(string username, string password)
        {
            var foundUser = _context.Users.FirstOrDefault(u => u.Username.ToLower() == username.ToLower() && u.Password == password);
            if (foundUser == null)
                return null;

            // Doing this strange thing because otherwise the User has a People collection, and each Person has a User reference, and it causes
            // a serialization error.  The only thing we really need for the app is to keep track of the UserId.
            return new User()
            {
                Username = foundUser.Username,
                UserId = foundUser.UserId
            };
        }

        public User CreateUser(string username, string password)
        {
            var user = new User() {  Username = username, Password = password };
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public Person CreatePerson(Person person)
        {
            var newPerson = _context.People.Add(person);
            _context.SaveChanges();
            return newPerson.Entity;
        }

        public void DeletePerson(int personId)
        {
            var person = _context.People.FirstOrDefault(p => p.PersonId == personId);
            if (person != null)
            {
                _context.People.Remove(person);
                _context.SaveChanges();
            }

        }

        public IEnumerable<Note> FindNotes(int userId, string search)
        {
            var people = GetPeople(userId);
            var peopleIds = _context.People.Where(p => p.UserId == userId).Select(p => p.PersonId).ToArray();
            return _context.Notes.Where(n => peopleIds.Contains(n.PersonId) && n.Text.Contains(search));
        }
    }
}
