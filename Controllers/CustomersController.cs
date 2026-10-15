using Microsoft.AspNetCore.Mvc;
using phoenix_warehouse.Data;
using phoenix_warehouse.models;

namespace phoenix_warehouse.Controllers;

[ApiController]
[Route("customers")]
public sealed class CustomersController(WarehouseDataStore dataStore) : ControllerBase
{
    [HttpGet]
    public ActionResult<List<Customer>> GetAll() => Ok(dataStore.Customers);

    [HttpGet("{customerId:guid}")]
    public ActionResult<Customer> GetById(Guid customerId)
    {
        var customer = dataStore.Customers.FirstOrDefault(item => item.Id == customerId);
        return customer is null ? NotFound() : Ok(customer);
    }

    [HttpPost]
    public IActionResult Create(Customer customer)
    {
        if (string.IsNullOrWhiteSpace(customer.FirstName) || string.IsNullOrWhiteSpace(customer.LastName))
        {
            return BadRequest(new { message = "Customer first and last name are required." });
        }

        dataStore.Customers.Add(customer);
        return CreatedAtAction(nameof(GetById), new { customerId = customer.Id }, customer);
    }
}
