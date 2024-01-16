using System.Security.Policy;

namespace Proiect.Models
{
    public class CreatedFoodItem
    {
        public int ChefID { get; set; }
        public int FoodID { get; set; }
        public Chef Chef { get; set; }
        public Food Food { get; set; }
    }
}
