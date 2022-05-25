using Microsoft.AspNetCore.Mvc;
using PeopleNotes.Models;
using System.Diagnostics;
using PeopleNotes.Data;

namespace PeopleNotes.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPeopleNotesRepository _repository;

        public HomeController(ILogger<HomeController> logger, IPeopleNotesRepository repo)
        {
            _logger = logger;
            _repository = repo;
        }

        [HttpPost]
        public IActionResult Index(IFormCollection f)
        {
            var search = f["search"].ToString();
            var people = _repository.GetPeopleByName(1, search);

            return View(people);
        }

        public IActionResult Index()
        {
            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddYears(1)
            };

            Response.Cookies.Append("UserId", "1", cookieOptions);
            return View();
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
    }
}