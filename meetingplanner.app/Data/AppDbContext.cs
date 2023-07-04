using Microsoft.EntityFrameworkCore;

namespace meetingplanner.meetingplanner.app.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        public DbSet<Speaker> Speakers { get; set; }
    }
}