using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_Submission_Site.Models;
using Project_Submission_Site.ViewModels;
using VerificationService;

namespace Project_Submission_Site.Controllers
{
	public class UserHomeController : Controller
	{
		ApplicationContext _context;

		public UserHomeController(ApplicationContext context)
		{
			_context = context;
		}

		[HttpGet]
		public IActionResult Home()
		{
			int? userid = HttpContext.Session.GetInt32("UserId");
			Account? account = null;

			if (userid != null && userid > 0)
				account = _context.Users.Where(x => x.Id == userid.Value).Include(x => x.Projects).FirstOrDefault();

			if (account == null)
				return RedirectToAction("Empty", "Login");
			else if (!account.Verified)
			{
				ViewBag.VerifyEmail = account.Email;
				return View("../Login/Empty");
			}
			var viewModel = new UserHomeViewModel()
			{
				Username = account.Username,
				Submitted = ((User)account).Projects.Where(p => p.Status == Status.Pending).ToList()
			};

			return View(viewModel);
		}

		[HttpGet]
		public IActionResult Logout()
		{
			int? userid = HttpContext.Session.GetInt32("UserId");
			if (userid != null && userid > 0)
				HttpContext.Session.Remove("UserId");
			return RedirectToAction("Empty", "Login");
		}
	}
}
