using Project_Submission_Site.Models;

namespace Project_Submission_Site.ViewModels
{
    public class ProjectViewModel
    {
        public Project Project { get; set; }
        public int RefereeId { get; set; }
        public List<Referee> Referees { get; set; }

        public String UserName { get; set; }
    }
}
