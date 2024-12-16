namespace Project_Submission_Site.Models
{
    public enum Status { Available, Working, Pending, Rejected, RequestModification, Accepted }
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Duration { get; set; }
        public float Budget { get; set; }
        public DateTime Deadline { get; set; }
        public Status Status { get; set; }


    }
}
