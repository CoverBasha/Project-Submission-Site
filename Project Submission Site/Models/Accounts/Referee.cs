namespace Project_Submission_Site.Models
{
    public class Referee : Account
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public Referee(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
