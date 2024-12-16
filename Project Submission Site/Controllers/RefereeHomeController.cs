using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_Submission_Site.Models;
using Project_Submission_Site.ViewModels;

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
				account = _context.Referees.Where(x => x.Id == userid.Value).Include(x => x.Projects).FirstOrDefault();

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
                Submitted = ((Referee)account).Projects.Where(p => p.Status == Status.Pending).ToList()
            };

            return View(viewModel);
        }

		[HttpGet]
		public IActionResult Approve(int id)
		{
			int? userid = HttpContext.Session.GetInt32("UserId");
			Account? account = null;

			if (userid != null && userid > 0)
				account = _context.Referees.Where(x => x.Id == userid.Value).Include(x => x.Projects).FirstOrDefault();

			if (account == null)
				return RedirectToAction("Empty", "Login");
			else if (!account.Verified)
			{
				ViewBag.VerifyEmail = account.Email;
				return View("../Login/Empty");
			}

			Project? project = null;
			if (id > 0)
				project = ((Referee)account).Projects.FirstOrDefault(x => x.Id == id);

			if (project != null)
			{
				project.Status = Status.Accepted;
				_context.SaveChanges();
			}
			return RedirectToAction("Home");
		}

		[HttpGet]
		public IActionResult Decline(int id)
		{
			int? userid = HttpContext.Session.GetInt32("UserId");
			Account? account = null;

			if (userid != null && userid > 0)
				account = _context.Referees.Where(x => x.Id == userid.Value).Include(x => x.Projects).FirstOrDefault();

			if (account == null)
				return RedirectToAction("Empty", "Login");
			else if (!account.Verified)
			{
				ViewBag.VerifyEmail = account.Email;
				return View("../Login/Empty");
			}

			Project? project = null;
			if (id > 0)
				project = ((Referee)account).Projects.FirstOrDefault(x => x.Id == id);

			if (project != null)
			{
				project.Status = Status.Rejected;
				_context.SaveChanges();
			}
			return RedirectToAction("Home");
		}
	}
}
