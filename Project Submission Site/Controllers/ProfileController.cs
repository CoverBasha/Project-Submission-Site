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

        public IActionResult Empty()
		{
			Account? account = SessionHelper.GetSession(this, _context);
			if (account == null)
				return RedirectToAction("Empty", "Login");


			return View(account);
		}

		[HttpPost]
		public IActionResult Update(Account account, string NewPassword, string ConfirmPassword)
		{

			return View();
		}
	}
}
