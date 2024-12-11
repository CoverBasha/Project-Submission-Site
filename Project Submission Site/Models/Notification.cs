namespace Project_Submission_Site.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public Project? Attachment { get; set; }
    }
}
