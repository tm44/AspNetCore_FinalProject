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
        public ActionResult Index(int id)
        {
            if (id == 0)
                return RedirectToAction("index", "home");

            var person = _repo.GetPersonById(CurrentUser.UserId, id);
            person.Notes = _repo.GetNotesForPerson(person.PersonId).ToList();
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
            return View();
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
