using IRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OrderProcessingSystem.Models;

namespace Repositories
{
    public class Repository : IRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<Repository> _logger;

        public Repository(ApplicationDbContext context, ILogger<Repository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<ActionResult<IEnumerable<Customer>>> Customers()
        {
            return await _context.Customers.Include(c => c.Orders).ToListAsync();
        }

        public async Task<ActionResult<Customer>> CustomerByCustomerId(int id)
        {
            var customer = await _context.Customers.Include(c => c.Orders).FirstOrDefaultAsync(c => c.Id == id);
            return customer;
        }

        public async Task<ActionResult<Order>> ConfigureOrder(int customerId, List<int> productIds)
        {
            var customer = await _context.Customers.Include(c => c.Orders).FirstOrDefaultAsync(c => c.Id == customerId);
            if (customer == null)
            {
                _logger.LogError("Customer not found.");
            }

            if (customer.Orders.Any(o => !o.IsFulfilled))
            {
                _logger.LogError("Previous order is unfulfilled.");
            }

            var products = await _context.Products.Where(p => productIds.Contains(p.Id)).ToListAsync();
            if (!products.Any())
            {
                _logger.LogError("Invalid products.");
            }

            var totalPrice = products.Sum(p => p.Price);
            var order = new Order
            {
                CustomerId = customerId,
                Products = products,
                TotalPrice = totalPrice,
                IsFulfilled = false
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return order;
        }

        public async Task<ActionResult<OrderDto>> OrderByOrderId(int id)
        {
            var order = await _context.Orders.Include(o => o.Products).FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                _logger.LogError("order not found.");
            }

            var totalPrice = order.Products.Sum(p => p.Price);

            var orderDto = new OrderDto
            {
                Id = order.Id,
                CustomerId = order.CustomerId,
                TotalPrice = totalPrice,
                IsFulfilled = order.IsFulfilled,
            };

            return orderDto;
        }

    }
}
