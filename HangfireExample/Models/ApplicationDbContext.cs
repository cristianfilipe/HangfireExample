using Microsoft.EntityFrameworkCore;
using HangfireExample.Models.Processing;

namespace HangfireExample.Models
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        
        public DbSet<ProcessingQueue> ProcessingQueue { get; set; }
    }
}
