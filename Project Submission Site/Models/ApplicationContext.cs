using Microsoft.EntityFrameworkCore;

namespace Project_Submission_Site.Models
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options)
        {

        }
    }
}
