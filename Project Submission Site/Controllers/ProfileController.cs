using Microsoft.AspNetCore.Mvc;
using Project_Submission_Site.Models;

namespace Project_Submission_Site.Controllers
{
	public class ProfileController : Controller
	{
		ApplicationContext _context;
        public ProfileController(ApplicationContext context)
        {
            _context = context;
        }
        public IActionResult Index()
		{

			return View();
		}
	}
}
