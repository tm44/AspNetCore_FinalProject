using PeopleNotes.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleNotes.Data
{
    public interface IPeopleNotesRepository
    {
        IEnumerable<Person> GetPeople(int userId);
        IEnumerable<Person> GetPeopleByName(int userId, string name);
        Person GetPersonById(int userId, int id);
        IEnumerable<Note> GetNotesForPerson(int personId);
    }
}
