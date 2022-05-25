using Microsoft.AspNetCore.Mvc;
using PeopleNotes.Classes;
using PeopleNotes.Data;

namespace PeopleNotes.Controllers
{
    public class NotesController : Controller
    {
        private readonly IPeopleNotesRepository _repo;
        public NotesController(IPeopleNotesRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
            {
                int personId = 0;
                int.TryParse(Request.Cookies["UserId"], out personId);
                var note = new Note() { PersonId = personId };
                return View("AddOrEdit", note);
            }
            else
            {
                var note = _repo.GetNoteById(id);
                return PartialView("AddOrEdit", note);
            }
        }

        [HttpPost]
        public IActionResult AddOrEdit(Note note)
        {
            if (ModelState.IsValid)
            {
                if (note.NoteId == 0)
                    _repo.CreateNote(note);
                else
                    _repo.UpdateNote(note);

                return RedirectToAction("index", "people", new { id = note.PersonId });
            }
            else
            {
                return View(note);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var note = _repo.GetNoteById(id);
            if (note != null)
                _repo.DeleteNote(id);

            return RedirectToAction("index", "people", new { id = note?.PersonId });
        }

    }
}
