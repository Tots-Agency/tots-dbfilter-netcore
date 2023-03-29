using Example.Entities;
using Microsoft.EntityFrameworkCore;

namespace Example.Persistence
{
    public class GreenwayContext : DbContext
    {
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Site> Sites { get; set; }
        public DbSet<Space> Spaces { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectAcknolegment> ProjectAcknolegments { get; set; }
        public DbSet<ProjectAcknolegmentItem> ProjectAcknolegmentItems { get; set; }
        public DbSet<ProjectInstall> ProjectInstalls { get; set; }
        public DbSet<ProjectPurchaseOrder> ProjectPurchaseOrders { get; set; }

        public GreenwayContext(DbContextOptions<GreenwayContext> options) : base(options)
        {

        }
    }
}
