using Microsoft.EntityFrameworkCore;
using UniConnectAPI.Models;

namespace UniConnectAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> Options) : base(Options) 
        {
            
        }
        public DbSet<Student> Students { get; set; }
    }
}
