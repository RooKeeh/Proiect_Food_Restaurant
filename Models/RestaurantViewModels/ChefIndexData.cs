using System.Security.Policy;

namespace Proiect.Models.RestaurantViewModels
{
    public class ChefIndexData
    {
        public IEnumerable<Chef>? Chefs { get; set; }
        public IEnumerable<Food>? Foods { get; set; }
        public IEnumerable<Order>? Orders { get; set; }
    }
}
