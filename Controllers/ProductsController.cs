using Microsoft.AspNetCore.Mvc;
using phoenix_warehouse.Data;
using phoenix_warehouse.models;

namespace phoenix_warehouse.Controllers;

[ApiController]
[Route("products")]
public sealed class ProductsController(WarehouseDataStore dataStore) : ControllerBase
{
    [HttpGet]
    public ActionResult<List<Product>> GetAll() => Ok(dataStore.Products);

    [HttpGet("{productId:guid}")]
    public ActionResult<Product> GetById(Guid productId)
    {
        var product = dataStore.Products.FirstOrDefault(item => item.Id == productId);
        return product is null ? NotFound() : Ok(product);
    }

    [HttpPost]
    public IActionResult Create(Product product)
    {
        if (string.IsNullOrWhiteSpace(product.SKU) || string.IsNullOrWhiteSpace(product.ProductName))
        {
            return BadRequest(new { message = "SKU and ProductName are required." });
        }

        product.IsAvailable = product.Quantity > 0 || product.IsAvailable;
        dataStore.Products.Add(product);
        return CreatedAtAction(nameof(GetById), new { productId = product.Id }, product);
    }
}
