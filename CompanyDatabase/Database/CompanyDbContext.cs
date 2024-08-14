using Microsoft.EntityFrameworkCore;
using CompanyDatabase.Models;

namespace CompanyDatabase.Database
{
    public class CompanyDbContext : DbContext
    {
        public CompanyDbContext(DbContextOptions<CompanyDbContext> options) :
            base(options)
        { }

        public DbSet<Customer> Customer { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Issue> Issue { get; set; }
        public DbSet<Branch> Branch { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderProduct> OrderProduct { get; set; }
        public DbSet<Stock> Stock { get; set; }
        public DbSet<Store> Store { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Customer - Orders (One to Many)
            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Orders)
                .WithOne(o => o.Customer)
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            // Order - Products (Many to Many with Quantity)
            modelBuilder.Entity<OrderProduct>()
                .HasKey(op => new { op.OrderId, op.ProductId });

            modelBuilder.Entity<OrderProduct>()
                .HasOne(op => op.Order)
                .WithMany(o => o.OrderProducts)
                .HasForeignKey(op => op.OrderId);

            modelBuilder.Entity<OrderProduct>()
                .HasOne(op => op.Product)
                .WithMany(p => p.OrderProducts)
                .HasForeignKey(op => op.ProductId);

            // Order - Issues (One to Many)
            modelBuilder.Entity<Order>()
                .HasMany(o => o.Issues)
                .WithOne(i => i.Order)
                .HasForeignKey(i => i.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // Remove previous Product - Issues (One to Many) configuration
            modelBuilder.Entity<Order>()
                .Ignore(o => o.Issues);  // Ignore the issues collection if previously configured

            // Category - Product (One to Many)
            modelBuilder.Entity<Category>()
                .HasMany(c => c.Products)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Branch - Employee (One to Many)
            modelBuilder.Entity<Branch>()
                .HasMany(b => b.Employees)
                .WithOne(e => e.Branch)
                .HasForeignKey(e => e.BranchId)
                .OnDelete(DeleteBehavior.Restrict);

            // Branch - Order (One to Many)
            modelBuilder.Entity<Branch>()
                .HasMany(b => b.Orders)
                .WithOne(o => o.Branch)
                .HasForeignKey(o => o.BranchId)
                .OnDelete(DeleteBehavior.Cascade);

            // Store - Store (Self referencing)
            modelBuilder.Entity<Store>()
                .HasOne(s => s.ParentStore)
                .WithMany(s => s.ChildStores)
                .HasForeignKey(s => s.ParentStoreId)
                .OnDelete(DeleteBehavior.Restrict);

            // Product - Stock (One to Many)
            modelBuilder.Entity<Product>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Product>()
                .HasMany(p => p.Stocks)
                .WithOne(st => st.Product)
                .HasForeignKey(st => st.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure the Stock entity
            modelBuilder.Entity<Stock>()
                .HasKey(st => st.Id);

            modelBuilder.Entity<Stock>()
                .HasIndex(st => new { st.ProductId, st.StoreId })
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
