namespace OrderProcessingSystem.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public ICollection<Product>? Products { get; set; }
        public decimal TotalPrice { get; set; }
        public bool IsFulfilled { get; set; }
    }
}
