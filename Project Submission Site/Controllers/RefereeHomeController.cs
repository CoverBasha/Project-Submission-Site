using Microsoft.AspNetCore.Mvc;
using Project_Submission_Site.Models;

namespace Project_Submission_Site.Controllers
{
    public class RefereeHomeController : Controller
    {
        Referee current;
        public IActionResult Home(Referee referee)
        {
            current = referee;
            return View();
        }
    }
}
