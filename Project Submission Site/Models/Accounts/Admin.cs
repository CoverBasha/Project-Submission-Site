namespace Project_Submission_Site.Models
{
    public class Admin : Account
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public Admin(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
