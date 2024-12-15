using Microsoft.AspNetCore.Mvc;
using Project_Submission_Site.Models;

namespace Project_Submission_Site.Controllers
{
    public class UserHomeController : Controller
    {
        User current;
        public IActionResult Home(User user)
        {
            current = user;
            return View(current);
        }
    }
}
