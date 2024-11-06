using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderProcessingSystem.Models;
using Repositories;
using Serilog;

namespace OrderProcessingSystem.Controllers
{
    [ApiController]
    [Route("api/orders")] 
    public class OrdersController : ControllerBase
    {
        private readonly Repository _Repository;
        private readonly ILogger<CustomersController> _logger;

        public OrdersController(Repository repository, ILogger<CustomersController> logger)
        {
            _Repository = repository;
            _logger = logger;
        }


        [HttpPost("CreateOrder")]
        public async Task<ActionResult<Order>> CreateOrder(int customerId, List<int> productIds)
        {
            try
            {
                var order = await _Repository.ConfigureOrder(customerId, productIds);

                return order;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating an order for customer {CustomerId}", customerId);
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }

        [HttpGet("{id}", Name = "GetOrderByOrderId")]
        public async Task<ActionResult<OrderDto>> GetOrderByOrderId(int id)
        {
            try
            {
                var order = await _Repository.OrderByOrderId(id);

                return order;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving order with ID {OrderId}", id);
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }
    }
}
