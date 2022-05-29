using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PeopleNotes.Classes;
using PeopleNotes.Data;
using PeopleNotes.Models;
using System.Security.Claims;

namespace PeopleNotes.Controllers
{
    public class PeopleController : BaseController
    {
        private IPeopleNotesRepository _repo;
        public PeopleController(IPeopleNotesRepository repo)
        {
            _repo = repo;
        }
        // GET: /People/1
        [Authorize]
        public ActionResult Index(int id, string sortOrder = "date")
        {
            if (id == 0)
                return RedirectToAction("index", "home");

            var person = _repo.GetPersonById(CurrentUser.UserId, id);

            if (sortOrder == "date")
                person.Notes = _repo.GetNotesForPerson(person.PersonId).OrderByDescending(n => n.LastUpdated).ToList();
            else
                person.Notes = _repo.GetNotesForPerson(person.PersonId).OrderBy(n => n.Text).ToList();

            return View(person);
        }

        // GET: PeopleController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PeopleController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PeopleController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PersonModel model)
        {

            if (!ModelState.IsValid)
                return View(model);

            var person = new Person()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserId = base.CurrentUser.UserId
            };

            _repo.CreatePerson(person);

            return RedirectToAction("index", new { id = person.PersonId});
        }

        // GET: PeopleController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PeopleController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PeopleController/Delete/5
        public ActionResult Delete(int id)
        {
            _repo.DeletePerson(id);
            return RedirectToAction("index");
        }

        // POST: PeopleController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
