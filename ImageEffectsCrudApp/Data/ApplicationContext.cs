using Microsoft.EntityFrameworkCore;

namespace ImageEffectsCrudApp.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
             : base(options)
        {
        }
        public DbSet<Image> Images { get; set; }
    }
}
