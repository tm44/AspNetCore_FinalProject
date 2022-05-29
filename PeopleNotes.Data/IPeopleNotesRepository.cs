using PeopleNotes.Classes;

namespace PeopleNotes.Data
{
    public interface IPeopleNotesRepository
    {
        IEnumerable<Person> GetPeople(int userId);
        IEnumerable<Person> GetPeopleByName(int userId, string name);
        Person GetPersonById(int userId, int id);
        IEnumerable<Note> GetNotesForPerson(int personId);
        Note GetNoteById(int noteId);
        Note CreateNote(Note note);
        Note UpdateNote(Note note);
        void DeleteNote(int noteId);
        User GetUser(string username, string password);
        User CreateUser(string username, string password);
        Person CreatePerson(Person person);
    }
}
