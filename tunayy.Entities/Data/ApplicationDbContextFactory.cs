using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace tunayy.Data
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=tunayy;Trusted_Connection=true");

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}