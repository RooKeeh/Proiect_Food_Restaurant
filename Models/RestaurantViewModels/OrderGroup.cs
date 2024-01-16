using System.ComponentModel.DataAnnotations;

namespace Proiect.Models.RestaurantViewModels
{
    public class OrderGroup
    {
        [DataType(DataType.Date)]
        public DateTime? OrderDate { get; set; }
        public int FoodCount { get; set; }
    }
}
