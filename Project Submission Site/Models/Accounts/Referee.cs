namespace Project_Submission_Site.Models
{
    public class Referee : Account
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public List<Project> Pending { get; set; }
        public Referee(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
