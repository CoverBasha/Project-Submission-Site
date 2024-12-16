using Microsoft.AspNetCore.Mvc;
using Project_Submission_Site.Models;

namespace Project_Submission_Site.Controllers
{
    public class RefereeHomeController : Controller
    {
		ApplicationContext _context;

		public RefereeHomeController(ApplicationContext context)
		{
			_context = context;
		}

		[HttpGet]
		public IActionResult Home()
		{
			int? userid = HttpContext.Session.GetInt32("UserId");
			Account? account = null;

			if (userid != null && userid > 0)
				account = _context.Referees.FirstOrDefault(x => x.Id == userid.Value);

			if (account == null)
				return RedirectToAction("Empty", "Login");
			else if (!account.Verified)
			{
				ViewBag.VerifyEmail = account.Email;
				return View("../Login/Empty");
			}
			return View(account);
		}
	}
}
