using Microsoft.EntityFrameworkCore;
using Proiect.Models;

namespace Proiect.Data
{
    public static class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new RestaurantContext(serviceProvider.GetRequiredService<DbContextOptions<RestaurantContext>>()))
            {
                if (context.Foods.Any())
                {
                    return;
                }

                context.Foods.AddRange(
                    new Food { Name = "Chicken Risotto", Description = "Creamy risotto with tender chicken pieces", Price = Decimal.Parse("14.99") },
                    new Food { Name = "Spaghetti Bolognese", Description = "Classic Italian pasta with rich meat sauce", Price = Decimal.Parse("12.99") },
                    new Food { Name = "Pizza Capricciosa", Description = "Pizza with tomato sauce, mozzarella, ham, mushroom and artichoke", Price = Decimal.Parse("16.99") }
                );

                context.Clients.AddRange(
                    new Client { Name = "John Doe", Email = "john.doe@example.com", Phone = "(555) 123-4567" },
                    new Client { Name = "Jane Smith", Email = "jane.smith@example.com", Phone = "(555) 987-6543" }
                );

                context.SaveChanges();
            }
        }
    }
}
