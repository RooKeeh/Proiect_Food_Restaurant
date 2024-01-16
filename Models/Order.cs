namespace Proiect.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public int ClientID { get; set; }
        public int FoodID { get; set; }
        public DateTime OrderDate { get; set; }
        public Client? Client { get; set; }
        public ICollection<Food> Foods { get; set; }
    }
}
