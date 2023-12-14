using FRp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FRp.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            int hour=DateTime.Now.Hour;
            string viewmodel = hour < 12 ? "������ ����" : "������ ����";
            return View("MyView",viewmodel);
        }
    }
}
