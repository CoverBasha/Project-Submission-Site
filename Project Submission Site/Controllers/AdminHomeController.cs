using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_Submission_Site.Models;
using Project_Submission_Site.ViewModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ServiceProvider.Helpers;

namespace Project_Submission_Site.Controllers
{
    public class AdminHomeController : Controller
    {
        ApplicationContext _context;

        public AdminHomeController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Home()
        {
            int? userid = HttpContext.Session.GetInt32("UserId");
            Account? account = null;

            if (userid != null && userid > 0)
                account = _context.Admins.FirstOrDefault(x => x.Id == userid.Value);

            if (account == null)
                return RedirectToAction("Empty", "Login");
            else if (!account.Verified)
            {
                ViewBag.VerifyEmail = account.Email;
                return View("../Login/Empty");
            }

            var viewModel = new AdminProjectViewModel()
            {
                Admin = (Admin)account,
                Projects = _context.Projects.ToList(),
            };

            return View(viewModel);
        }

        public IActionResult ProjectForm()
        {
            var viewModel = new ProjectViewModel()
            {
                Referees = _context.Referees.ToList(),
                UserName = SessionHelper.GetSession(this, _context).Username,
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Save(ProjectViewModel viewModel)
        {
            if (viewModel.Project.Id == 0)
            {
                _context.Projects.Add(viewModel.Project);
            }
            else
            {
                var newProject = _context.Projects.Single(p => p.Id == viewModel.Project.Id);

                newProject.Name = viewModel.Project.Name;
                newProject.Budget = viewModel.Project.Budget;
                newProject.Deadline = viewModel.Project.Deadline;
                newProject.Duration = viewModel.Project.Duration;
                newProject.Status = Status.Available;
            }

            _context.SaveChanges();

            return RedirectToAction("Home");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var project = _context.Projects.SingleOrDefault(x => x.Id == id);

            if (project == null)
                return NotFound();

            var viewModel = new ProjectViewModel()
            {
                Project = project,
                Referees = _context.Referees.ToList(),
                UserName = SessionHelper.GetSession(this, _context).Username,
            };

            return View("ProjectForm", viewModel);
        }

        public IActionResult Delete(int id)
        {
            _context.Remove(_context.Projects.Single(x => x.Id == id));
            _context.SaveChanges();

            return RedirectToAction("Home");
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
