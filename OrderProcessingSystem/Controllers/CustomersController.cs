using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderProcessingSystem.Models;
using Repositories;

namespace OrderProcessingSystem.Controllers
{
    public class CustomersController : Controller
    {
        private readonly Repository _Repository; 
        private readonly ILogger<CustomersController> _logger;

        public CustomersController(Repository repository, ILogger<CustomersController> logger)
        {
            _Repository = repository;
            _logger = logger;
        }

        [HttpGet("GetCustomers")]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            try
            {
                var customerslist = await _Repository.Customers();
                return customerslist;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving customers.");
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }

        [HttpGet("GetCustomerByCustomerId")]
        public async Task<ActionResult<Customer>> GetCustomerByCustomerId(int id)
        {
            try
            {
                var CustomerByCustomerId = await _Repository.CustomerByCustomerId(id);
                if (CustomerByCustomerId == null)
                {
                    return NotFound();
                }
                return CustomerByCustomerId;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving customer with ID {CustomerId}", id);
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }
    }
}
