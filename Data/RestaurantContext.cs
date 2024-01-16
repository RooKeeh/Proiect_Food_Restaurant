using Microsoft.EntityFrameworkCore;
using Proiect.Models;
using System.Security.Policy;

namespace Proiect.Data
{
    public class RestaurantContext : DbContext
    {
        public RestaurantContext(DbContextOptions<RestaurantContext> options) : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<Models.Chef> Chefs { get; set; }
        public DbSet<CreatedFoodItem> CreatedFoodItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>().ToTable("Client");
            modelBuilder.Entity<Order>().ToTable("Order");
            modelBuilder.Entity<Food>().ToTable("Food");
            modelBuilder.Entity<Models.Chef>().ToTable("Chef");
            modelBuilder.Entity<CreatedFoodItem>().ToTable("CreatedFoodItem");
            modelBuilder.Entity<CreatedFoodItem>()
            .HasKey(c => new { c.FoodID, c.ChefID });
        }
    }
}
