namespace OrderProcessingSystem.Models
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public decimal TotalPrice { get; set; }
        public bool IsFulfilled { get; set; }
        public List<Product>? Products { get; set; }
    }

}
