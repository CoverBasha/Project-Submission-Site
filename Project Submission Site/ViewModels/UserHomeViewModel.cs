using Project_Submission_Site.Models;

namespace Project_Submission_Site.ViewModels
{
	public class UserHomeViewModel
	{
		public string Username { get; set; }
		public List<Project> Submitted { get; set; }


	}
}
