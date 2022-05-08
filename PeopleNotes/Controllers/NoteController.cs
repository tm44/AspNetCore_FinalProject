using Microsoft.AspNetCore.Mvc;
using PeopleNotes.Classes;
using PeopleNotes.Data;

namespace PeopleNotes.Controllers
{
    public class NoteController : Controller
    {
        private readonly IPeopleNotesRepository _repo;
        public NoteController(IPeopleNotesRepository repo)
        {
            _repo = repo;
        }
        [HttpGet]
        public IActionResult Create(int id)
        {
            var note = new Note();
            note.PersonId = id;
            return PartialView("_Note", note);
        }

        [HttpPost]
        public IActionResult Create([FromBody]Note note)
        {
            note.LastUpdated = DateTime.Now;

            if (ModelState.IsValid)
            {
                var savednote = _repo.CreateNote(note);
                return PartialView("_Note", savednote);
            }
            else
                return PartialView("_Note", note);

        }
    }
}
