﻿using PeopleNotes.Classes;

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
    }
}
