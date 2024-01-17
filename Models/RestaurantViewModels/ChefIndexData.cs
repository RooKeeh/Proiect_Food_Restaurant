using System.ComponentModel.DataAnnotations;
using System.Security.Policy;

namespace Proiect.Models.RestaurantViewModels
{
    public class ChefIndexData
    {
        [Display(Name = "Chef Name")]
        public IEnumerable<Chef>? Chefs { get; set; }
        public IEnumerable<Food>? Foods { get; set; }
        public IEnumerable<Order>? Orders { get; set; }
    }
}
