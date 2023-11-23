namespace SignalR.API.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
        public decimal OpenPrice {  get; set; } 
    }
}
