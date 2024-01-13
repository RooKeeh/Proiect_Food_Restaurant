namespace Proiect.Models
{
    public class Order
    {
        public int ID { get; set; }
        public DateTime OrderDate { get; set; }
        public string Client { get; set; }
        public ICollection<Food> Foods { get; set; }
    }
}
