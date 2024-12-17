using Microsoft.AspNetCore.Mvc;
using System.IO;
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

		public IActionResult Projects()
        {
			return View(_context.Projects.Where(x => x.Status == Status.Available).ToList());
        }

		[HttpGet]
		public IActionResult ProjectDetail(int id)
        {
			return View(_context.Projects.SingleOrDefault(p => p.Id == id));
        }

		[HttpPost]
		public IActionResult SubmitProject(int id, string file)
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

			Project? project = null;
			if (id > 0)
				project = _context.Projects.FirstOrDefault(x => x.Id == id);

			if (project != null)
			{
				Directory.CreateDirectory("Proposals");
				//System.IO.File.Copy(file, String.Format("Proposals/{0}", System.IO.Path.GetFileName(file)));

				Project project1 = project;
				project.Status = Status.Pending;

				((User)account).Projects.Add(project);
				_context.SaveChanges();

				return RedirectToAction("Home");
			}
			return View("ProjectDetail", id);
		}
	}
}
