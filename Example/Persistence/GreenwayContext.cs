using Example.Entities;
using Microsoft.EntityFrameworkCore;

namespace Example.Persistence
{
    public class GreenwayContext : DbContext
    {
        public DbSet<Contract> Contracts { get; set; }

        public GreenwayContext(DbContextOptions<GreenwayContext> options) : base(options)
        {

        }
    }
}
