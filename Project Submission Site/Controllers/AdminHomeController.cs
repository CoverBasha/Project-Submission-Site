using Microsoft.AspNetCore.Mvc;
using Project_Submission_Site.Models;

namespace Project_Submission_Site.Controllers
{
    public class AdminHomeController : Controller
    {
        Admin current;
        public IActionResult Home(Admin admin)
        {
            current = admin;
            return View();
        }
    }
}
