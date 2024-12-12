namespace Project_Submission_Site.Models
{
    public class User : Account
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Stack<Notification> Notifications { get; set; }
        public List<Project> Projects { get; set; }

        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
