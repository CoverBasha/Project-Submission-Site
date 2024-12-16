using Microsoft.AspNetCore.Mvc;
using Project_Submission_Site.Models;
using ServiceProvider.Helpers;

namespace Project_Submission_Site.Controllers
{
	public class ProfileController : Controller
	{
		ApplicationContext _context;
        public ProfileController(ApplicationContext context)
        {
            _context = context;
        }
        public IActionResult Details()
		{
			var user = SessionHelper.GetSession(this, _context);
			return View(user);
		}
	}
}
