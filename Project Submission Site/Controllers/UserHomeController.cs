using Microsoft.AspNetCore.Mvc;
using Project_Submission_Site.Models;

namespace Project_Submission_Site.Controllers
{
    public class UserHomeController : Controller
    {
        ApplicationContext _context;
        User current;

        public UserHomeController(ApplicationContext context)
        {
            _context = context;
        }
        public IActionResult Home(User user)
        {
            current = user;
            return View(current);
        }

        public IActionResult Projects()
        {
            var projects = _context.Projects.Where(p => p.Status == 0).ToList();
            return View(projects);
        }

        public IActionResult Profile()
        {
            return View(current);
        }

        public IActionResult Logout()
        {
            current = null;
            return RedirectToAction("Empty", "Login");
        }
    }
}
