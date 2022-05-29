using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using PeopleNotes.Classes;
using PeopleNotes.Data;
using Newtonsoft.Json;

namespace PeopleNotes.Controllers
{
    public class BaseController : Controller
    {
        public User CurrentUser
        {
            get
            {
                if (HttpContext.Session.GetString("User") != null)
                {
                    return JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("User"));
                }
                return null;
            }
        }
    }
}