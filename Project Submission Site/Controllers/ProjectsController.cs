using Microsoft.AspNetCore.Mvc;
using Project_Submission_Site.Models;
using ServiceProvider.Helpers;

namespace Project_Submission_Site.Controllers
{
    public class ProjectsController : Controller
    {
        ApplicationContext _context;
        public ProjectsController(ApplicationContext context)
        {
            _context = context;

        }
        public IActionResult Projects()
        {
            var projects = _context.Projects.ToList();
            return View(projects);
        }

        public ActionResult Details(int id)
        {
            var project = _context.Projects.SingleOrDefault(p => p.Id == id);

            return View(project);
        }
    }
}
