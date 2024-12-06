using Microsoft.EntityFrameworkCore;

namespace Project_Submission_Site.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Referee> Referees { get; set; }

        public ApplicationContext(DbContextOptions options) : base(options)
        {

        }
    }
}
