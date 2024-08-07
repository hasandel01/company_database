using Microsoft.EntityFrameworkCore;
using CompanyDatabase.Models;

namespace CompanyDatabase.Database
{
    public class CompanyDbContext: DbContext
    {
        public CompanyDbContext(DbContextOptions options): 
            base(options) { }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Issue> Issues { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Company - Product (One to Many)
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Company)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);

            // Product - Issues (One to Many)
            modelBuilder.Entity<Product>()
                .HasMany(p => p.Issues)
                .WithOne(i => i.Product)
                .HasForeignKey(i => i.ProductId)
                .OnDelete(DeleteBehavior.Restrict);


            // Category - Product (One to Many)
            modelBuilder.Entity<Category>()
                .HasMany(c => c.Products)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Company>()
                .HasMany(c => c.Employees)
                .WithOne(e => e.Company)
                .HasForeignKey(e => e.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);
     

                base.OnModelCreating(modelBuilder);
        }

    }
  
}
