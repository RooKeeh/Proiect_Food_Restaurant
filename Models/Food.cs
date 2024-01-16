using System.ComponentModel.DataAnnotations.Schema;

namespace Proiect.Models
{
    public class Food
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        [Column(TypeName = "decimal(6, 2)")]
        public decimal Price { get; set; }
        public ICollection<Order>? Orders { get; set; }
        public ICollection<CreatedFoodItem>? CreatedFoodItems { get; set; }
    }
}
