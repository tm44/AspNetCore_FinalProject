using Microsoft.AspNetCore.Mvc;
using PeopleNotes.Models;
using System.Diagnostics;
using PeopleNotes.Data;
using Microsoft.AspNetCore.Authorization;

namespace PeopleNotes.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPeopleNotesRepository _repository;

        public HomeController(ILogger<HomeController> logger, IPeopleNotesRepository repo)
        {
            _logger = logger;
            _repository = repo;
        }


        [HttpGet]
        [Authorize]
        public IActionResult Index()
        {
            if (CurrentUser == null)
                return RedirectToAction("LogOn", "Account");
            var people = _repository.GetPeople(CurrentUser.UserId);
            return View(people);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult Search(string search)
        {
            var notes = _repository.FindNotes(CurrentUser.UserId, search);
            return PartialView("_Search", notes);
        }
    }
}