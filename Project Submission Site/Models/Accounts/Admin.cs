namespace Project_Submission_Site.Models
{
    public class Admin : Account
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Admin(string username, string password)
        {
            Username = username;
            Password = password;
        }

    }
}
