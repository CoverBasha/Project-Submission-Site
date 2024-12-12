using Microsoft.AspNetCore.Mvc;
using Project_Submission_Site.Models;

namespace Project_Submission_Site.Controllers
{
    public class ProjectsController : Controller
    {
       
        public ProjectsController()
        {
        }
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            return View();
        }
    }
}
