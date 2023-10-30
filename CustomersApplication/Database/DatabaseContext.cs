using Microsoft.EntityFrameworkCore;

namespace CustomersApplication.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        { }

        public DbSet<AccountDB> Accounts { get; set; }
        public DbSet<CustomerDB> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountDB>().ToTable("Account");
            modelBuilder.Entity<CustomerDB>().ToTable("Customer", "dbo");

           //base.OnModelCreating(modelBuilder);
        }
    }
}
