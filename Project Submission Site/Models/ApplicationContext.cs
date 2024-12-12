using Microsoft.EntityFrameworkCore;

namespace Project_Submission_Site.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }
        public ApplicationContext(DbContextOptions options) : base(options)
        {

        }
    }
}
