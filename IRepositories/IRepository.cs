using Microsoft.AspNetCore.Mvc;
using OrderProcessingSystem.Models;

namespace IRepositories
{
    public interface IRepository
    {
        public Task<ActionResult<IEnumerable<Customer>>> Customers();
        public Task<ActionResult<Customer>> CustomerByCustomerId(int id);
        public Task<ActionResult<Order>> ConfigureOrder(int customerId, List<int> productIds);
        public Task<ActionResult<OrderDto>> OrderByOrderId(int id);
    }
}
