using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using PeopleNotes.Classes;
using PeopleNotes.Data;
using Newtonsoft.Json;
using PeopleNotes.Models;

namespace PeopleNotes.Controllers
{
    public class AccountController : Controller
    {
        private IPeopleNotesRepository _repo;
        public AccountController(IPeopleNotesRepository repo)
        {
            _repo = repo;
        }
        [HttpGet]
        public IActionResult LogOut()
        {
            // Get rid of the authentication cookie
            HttpContext.SignOutAsync(
    CookieAuthenticationDefaults.AuthenticationScheme);

            // Go to home page
            return Redirect("~/");
        }

        [HttpGet]
        public IActionResult LogOn()
        {
            // Keep track of returnUrl
            ViewData["ReturnUrl"] = Request.Query["returnUrl"];
            return View();
        }

        [HttpPost]
        public IActionResult LogOn(User model, string? returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = _repo.GetUser(model.Username, model.Password);
                if (user == null)
                    return View(model);
                return LoginUser(user, returnUrl);
            }

            ViewData["ReturnUrl"] = returnUrl;

            return View(model);
        }

        private IActionResult LoginUser(User user, string? returnUrl)
        {
            var json = JsonConvert.SerializeObject(user);
            HttpContext.Session.SetString("User", json);

            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                };

            var claimsIdentity = new ClaimsIdentity(claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            var authProperties = new AuthenticationProperties
            {
                AllowRefresh = false,
                // Refreshing the authentication session should be allowed.

                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                // The time at which the authentication ticket expires. A 
                // value set here overrides the ExpireTimeSpan option of 
                // CookieAuthenticationOptions set with AddCookie.

                IsPersistent = user.RememberMe,
                // Whether the authentication session is persisted across 
                // multiple requests. When used with cookies, controls
                // whether the cookie's lifetime is absolute (matching the
                // lifetime of the authentication ticket) or session-based.

                IssuedUtc = DateTimeOffset.UtcNow,
                // The time at which the authentication ticket was issued.
            };

            HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                claimsPrincipal,
                authProperties).Wait();

            return Redirect(returnUrl ?? "~/");
        }

        public ActionResult LogOff()
        {
            HttpContext.Session.Remove("User");

            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return Redirect("~/");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = _repo.CreateUser(model.Username, model.Password);

            return LoginUser(user, "~/");
        }
    }
}