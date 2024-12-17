using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Project_Submission_Site.Models;
using Project_Submission_Site.ViewModels;
using System.Net.Mail;
using VerificationService;

namespace Project_Submission_Site.Controllers
{
	public class RefereeHomeController : Controller
	{
		ApplicationContext _context;
		GmailVerifyClient _client;

		public RefereeHomeController(ApplicationContext context)
		{
			_context = context;
			_client = new GmailVerifyClient();
        }

		[NonAction]
		private IActionResult? IsLoggedIn()
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
			return null;
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

		[HttpGet]
		public IActionResult Modify(int id, string desc)
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
				RefereeModifyViewModel viewModel = new RefereeModifyViewModel()
				{
					Project = project,
					Description = desc,
				};

                return View(viewModel);
			}
            return View(null);
        }

		[HttpPost]
		public IActionResult PostModify(int id, string desc)
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

			if (project != null
				&& ModelState.GetValidationState("id") == ModelValidationState.Valid
                && ModelState.GetValidationState("desc") == ModelValidationState.Valid)
			{
				var user = _context.Users
					.Include(x => x.Projects)
					.Where(x => x.Projects.Count(x => x.Id == project.Id) >= 1)
					.Take(1)
					.FirstOrDefault();

				if (user != null)
				{
					_client.SendModificationNotify(user.Email, user.Username, project.Name, desc);

                    project.Status = Status.RequestModification;

					user.Notifications.Add(new Notification
					{
                        Description = desc,
                        Date = DateTime.Now,
                        Attachment = project,
                    });
					_context.SaveChanges();

					ViewBag.ProjectName = project.Name;
					return View("ModifySubmitted");
				}
				else
					ModelState.AddModelError("Description", "Couldn't find user who submitted this project.");
			}

            RefereeModifyViewModel viewModel = new RefereeModifyViewModel()
            {
                Project = project,
                Description = desc,
            };
            return View("Modify", viewModel);
        }

		public IActionResult Logout()
		{
			int? userid = HttpContext.Session.GetInt32("UserId");
			if (userid != null && userid > 0)
				HttpContext.Session.Remove("UserId");
			return RedirectToAction("Empty", "Login");
		}
	}
}
