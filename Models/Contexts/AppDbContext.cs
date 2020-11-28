using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GasStation.Models.Contexts
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProductsList>().HasKey(pl => new { pl.ProductId, pl.TransactionId });
            modelBuilder.Entity<ProductsList>().HasOne<Transaction>(pl => pl.Transaction).WithMany(t => t.ProductsLists).HasForeignKey(pl => pl.TransactionId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<ProductsList>().HasOne<Product>(pl => pl.Product).WithMany(p => p.ProductsLists).HasForeignKey(pl => pl.ProductId).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<TankDistributor>().HasKey(td => new { td.TankId, td.DistributorId });
            modelBuilder.Entity<TankDistributor>().HasOne<Tank>(td => td.Tank).WithMany(t => t.TankDistributors).HasForeignKey(td => td.TankId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<TankDistributor>().HasOne<Distributor>(td => td.Distributor).WithMany(d => d.TankDistributors).HasForeignKey(td => td.DistributorId).OnDelete(DeleteBehavior.NoAction);
        }

        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Distributor> Distributors { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Fueling> Fuelings { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<LoyaltyCard> LoyaltyCards { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductsList> ProductsLists { get; set; }
        public DbSet<Tank> Tanks { get; set; }
        public DbSet<TankDistributor> TankDistributors { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}
