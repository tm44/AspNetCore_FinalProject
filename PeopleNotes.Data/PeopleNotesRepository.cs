using PeopleNotes.Classes;

namespace PeopleNotes.Data
{
    public class PeopleNotesRepository : IPeopleNotesRepository
    {
        public IEnumerable<Person> GetPeopleByName(int userId, string name)
        {
            return new PeopleNotesContext().People.AsEnumerable<Person>().Where(p => 
                p.UserId == userId &&
                isNameMatch(p, name));
        }

        public IEnumerable<Note> GetNotesForPerson(int personId)
        {
            return new PeopleNotesContext().Notes.Where(n => n.PersonId == personId);
        }

        public IEnumerable<Person> GetPeople(int userId)
        {
            return new PeopleNotesContext().People.Where(p => p.UserId == userId);
        }

        private bool isNameMatch(Person person, string search)
        {
            return person.FirstName.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                (!string.IsNullOrEmpty(person.LastName) && person.LastName.Contains(search, StringComparison.OrdinalIgnoreCase));
        }

        public Person GetPersonById(int userId, int id)
        {
            return new PeopleNotesContext().People.First(p => p.PersonId == id);
        }
    }
}
