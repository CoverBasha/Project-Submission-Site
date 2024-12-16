using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_Submission_Site.Models;
using Project_Submission_Site.ViewModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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
            return View();
        }

        [HttpPost]
        public IActionResult Save(Project project)
        {
            if (!ModelState.IsValid)
                return View("ProjectForm", project);

            if (project.Id == 0)
            {
                _context.Projects.Add(project);
            }
            else
            {
                var newProject = _context.Projects.Single(p => p.Id == project.Id);

                newProject.Name = project.Name;
                newProject.Budget = project.Budget;
                newProject.Deadline = project.Deadline;
                newProject.Duration = project.Duration;
                newProject.Status = Status.Available;
            }

            _context.SaveChanges();

            return RedirectToAction("Home");
        }

        [HttpPost]
        public IActionResult Edit(int id)
        {
            var project = _context.Projects.SingleOrDefault(x => x.Id == id);

            if (project == null)
                return NotFound();

            return View("CustomerForm", project);
        }

        public IActionResult Projects()
        {
            var projects = _context.Projects.ToList();
            return View(projects);
        }
    }
}
