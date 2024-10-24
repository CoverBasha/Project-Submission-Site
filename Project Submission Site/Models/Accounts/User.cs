namespace Project_Submission_Site.Models
{
    public class User : Account
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
