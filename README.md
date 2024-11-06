# Manish_OrderProcessingSystem

This is a simplified Order Processing System for an e-commerce application built with .NET Core. It provides functionality for managing customers, products, and orders, with an emphasis on creating orders and calculating total prices based on associated products.

## Project Setup Instructions

### Prerequisites
- [.NET 5+ SDK]
- [SQL Server]
- [Entity Framework Core CLI]

### Setup Instructions
1. **Clone the Repository**:
   ```bash
   git clone https://github.com/ManishJujum/Manish_OrderProcessingSystem.git
   cd OrderProcessingSystem


## Assumptions or Design Decisions
Database Relations:
Each Customer can have multiple Orders.
Each Order can contain multiple Products.

Order Fulfillment Check:
New orders can only be placed if the customer’s previous order is fulfilled (IsFulfilled set to true).

Total Price Calculation:
The total price of an order is calculated based on the sum of the prices of associated Product items.

## API Endpoints Documentation
Customers API :
GET/api/GetCustomers
Retrieves a list of all customers.
Response: Array of customers, each including their associated orders.

GET /api/GetCustomerByCustomerId
Retrieves details of a specific customer by ID, including their orders.
Response: Customer object with a list of orders.

Orders API :
POST /api/orders/CreateOrder
Creates a new order for a specified customer.
Parameters:
customerId: The ID of the customer placing the order.
productIds: A list of product IDs to be included in the order.
Response: Created order object, including the calculated total price.

GET /api/orders/{id}
Retrieves details for a specific order by ID, including the total price.
Response: Order object, including a list of products and the total price.

## Additional Notes for Reviewers
Logging:
This project uses Serilog for logging API requests and errors to the console.
Logs are helpful in tracking requests and diagnosing validation or database issues.

Data Validation:
Basic validation is implemented to ensure valid data is passed to endpoints (e.g., existing customerId and valid productIds for orders).
