using Microsoft.EntityFrameworkCore;
using Proiect.Models;
using System.Security.Policy;

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

                var orders = new Order[]
                {
                    new Order{FoodID=1,ClientID=1,OrderDate=DateTime.Parse("2024-01-05")},
                    new Order{FoodID=2,ClientID=2,OrderDate=DateTime.Parse("2023-09-07")},
                    new Order{FoodID=3,ClientID=1,OrderDate=DateTime.Parse("2023-10-27")},
                    new Order{FoodID=1,ClientID=1,OrderDate=DateTime.Parse("2023-05-30")},
                    new Order{FoodID=2,ClientID=1,OrderDate=DateTime.Parse("2024-01-01")},
                    new Order{FoodID=2,ClientID=2,OrderDate=DateTime.Parse("2023-12-12")},
                };
                foreach (Order e in orders)
                {
                    context.Orders.Add(e);
                }
                context.SaveChanges();

                var chefs = new Models.Chef[]
                {
                    new Models.Chef{ChefName="Alex Smith"},
                    new Models.Chef{ChefName="Jamie Moore"},
                    new Models.Chef{ChefName="Chris Anderson"},
                };
                foreach (Models.Chef c in chefs)
                {
                    context.Chefs.Add(c);
                }
                context.SaveChanges();

                var foods = context.Foods;
                /*var createdFoodItems = new CreatedFoodItem[]
                {
                    new CreatedFoodItem {FoodID = foods.Single(c => c.Name == "Chicken Risotto" ).ID, ChefID = chefs.Single(i => i.ChefName == "Alex Smith").ID},
                    new CreatedFoodItem {FoodID = foods.Single(c => c.Name == "Spaghetti Bolognese" ).ID, ChefID = chefs.Single(i => i.ChefName == "Chris Anderson").ID},
                    new CreatedFoodItem {FoodID = foods.Single(c => c.Name == "Spaghetti Bolognese" ).ID, ChefID = chefs.Single(i => i.ChefName == "Jamie Moore").ID},
                    new CreatedFoodItem {FoodID = foods.Single(c => c.Name == "Chicken Risotto" ).ID, ChefID = chefs.Single(i => i.ChefName == "Alex Smith").ID},
                    new CreatedFoodItem {FoodID = foods.Single(c => c.Name == "Pizza Capricciosa" ).ID, ChefID = chefs.Single(i => i.ChefName == "Alex Smith").ID},
                    new CreatedFoodItem {FoodID = foods.Single(c => c.Name == "Spaghetti Bolognese" ).ID, ChefID = chefs.Single(i => i.ChefName == "Chris Anderson").ID},
                };
                foreach (CreatedFoodItem cfi in createdFoodItems)
                {
                    context.CreatedFoodItems.Add(cfi);
                }*/
                context.SaveChanges();
            }
        }
    }
}
