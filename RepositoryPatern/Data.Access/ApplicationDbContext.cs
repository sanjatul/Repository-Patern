using Microsoft.EntityFrameworkCore;
using RepositoryPatern.Models;

namespace RepositoryPatern.Data.Access
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
    }
}
