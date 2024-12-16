namespace Project_Submission_Site.Models
{
    public class Referee : Account
    {
        /*
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        */
        public List<Project> Projects { get; set; }
        public Referee()
        {
            Projects = new List<Project>();
        }
    }
}
