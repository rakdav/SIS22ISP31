using Microsoft.AspNetCore.Mvc;
using PartyInvites.Models;
using System.Diagnostics;

namespace PartyInvites.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ViewResult RSVPForm()
        {
            return View();
        }
        [HttpPost]
        public ViewResult RSVPForm(GuestResponse guestResponse)
        {
            if (ModelState.IsValid)
            {
                Repository.AddResponse(guestResponse);
                return View("Thanks", guestResponse);
            }
            return View();
        }
        public ViewResult ListResponse()
        {
            return View(Repository.Response.Where(p=>p.WillAttend==true));
        }
    }
}
