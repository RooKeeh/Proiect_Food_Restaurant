using System.ComponentModel.DataAnnotations;

namespace Proiect.Models
{
    public class Chef
    {
        public int ID { get; set; }
        [Required]
        [Display(Name = "Chef Name")]
        [StringLength(50)]
        public string? ChefName { get; set; }   
        public ICollection<CreatedFoodItem>? CreatedFoodItems { get; set; }

    }
}
