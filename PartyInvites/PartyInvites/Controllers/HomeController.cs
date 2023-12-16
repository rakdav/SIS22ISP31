using Microsoft.AspNetCore.Mvc;
using PartyInvites.Models;
using System.Diagnostics;

namespace PartyInvites.Controllers
{
    public class HomeController : Controller
    {
        ApplicationContext db;
        public HomeController(ApplicationContext context)
        {
            db = context;
        }
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
                db.GuestResponses.Add(guestResponse);
                db.SaveChangesAsync();
                Repository.AddResponse(guestResponse);
                return View("Thanks", guestResponse);
            }
            return View();
        }
        public ViewResult ListResponse()
        {
            return View(db.GuestResponses.Where(p=>p.WillAttend==true));
        }
    }
}
