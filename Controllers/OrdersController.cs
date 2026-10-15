using Microsoft.AspNetCore.Mvc;
using phoenix_warehouse.Data;
using phoenix_warehouse.models;

namespace phoenix_warehouse.Controllers;

[ApiController]
[Route("orders")]
public sealed class OrdersController(WarehouseDataStore dataStore) : ControllerBase
{
    [HttpGet]
    public ActionResult<List<Order>> GetAll() => Ok(dataStore.Orders);

    [HttpGet("{orderId:guid}")]
    public ActionResult<Order> GetById(Guid orderId)
    {
        var order = dataStore.Orders.FirstOrDefault(item => item.Id == orderId);
        return order is null ? NotFound() : Ok(order);
    }

    [HttpPost]
    public IActionResult Create(CreateOrderRequest request)
    {
        if (request.Items is null || request.Items.Count == 0)
        {
            return BadRequest(new { message = "At least one order item is required." });
        }

        var customerExists = dataStore.Customers.Any(customer => customer.Id == request.CustomerId);
        if (!customerExists)
        {
            return BadRequest(new { message = "CustomerId must reference a valid customer." });
        }

        var order = new Order { CustomerId = request.CustomerId, Status = "Pending" };
        foreach (var item in request.Items)
        {
            var product = dataStore.Products.FirstOrDefault(candidate => candidate.Id == item.ProductId);
            if (product is null)
            {
                return BadRequest(new { message = $"Product {item.ProductId} not found." });
            }

            if (item.Quantity <= 0)
            {
                return BadRequest(new { message = "Order quantities must be greater than zero." });
            }

            order.AddItem(product, item.Quantity);
        }

        dataStore.Orders.Add(order);
        return CreatedAtAction(nameof(GetById), new { orderId = order.Id }, order);
    }

    [HttpPatch("{orderId:guid}/status")]
    public IActionResult UpdateStatus(Guid orderId, OrderStatusUpdateRequest request)
    {
        var order = dataStore.Orders.FirstOrDefault(item => item.Id == orderId);
        if (order is null)
        {
            return NotFound();
        }

        order.UpdateStatus(request.Status);
        return Ok(order);
    }
}
