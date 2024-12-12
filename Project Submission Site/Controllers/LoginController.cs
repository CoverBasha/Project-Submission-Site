using Microsoft.AspNetCore.Mvc;
using Project_Submission_Site.Models;

namespace Project_Submission_Site.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Empty()
        {
            return View();
        }

        public ActionResult Login()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}
