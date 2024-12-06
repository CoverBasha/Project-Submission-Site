using Microsoft.AspNetCore.Mvc;
using Project_Submission_Site.Models;

namespace Project_Submission_Site.Controllers
{
    public class ProjectsController : Controller
    {
        List<Project> projects;
        public ProjectsController()
        {
            projects = new List<Project>()
            {
                new Project()
                {
                    Name ="Nigga",
                    Id = 1,
                    Budget = 2000,
                    Deadline = DateTime.Now,
                    Duration = 2
                },
                new Project()
                {
                    Name ="Niggerism",
                    Id = 2,
                    Budget = 2000,
                    Deadline = DateTime.Now,
                    Duration = 2
                }
            };
        }
        public IActionResult Index()
        {
            return View(projects);
        }

        public ActionResult Details(int id)
        {
            var project = projects.Where(p => p.Id == id).FirstOrDefault();
            return View(project);
        }
    }
}
