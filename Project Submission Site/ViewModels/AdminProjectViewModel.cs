using Project_Submission_Site.Models;

namespace Project_Submission_Site.ViewModels
{
    public class AdminProjectViewModel
    {
        public Admin Admin { get; set; }
        public IEnumerable<Project> Projects { get; set; }

    }
}
