using Microsoft.AspNetCore.Mvc;
using phoenix_warehouse.Data;
using phoenix_warehouse.models;

namespace phoenix_warehouse.Controllers;

[ApiController]
[Route("inventory")]
public sealed class InventoryController(WarehouseDataStore dataStore) : ControllerBase
{
    [HttpGet]
    public ActionResult<List<Inventory>> GetAll() => Ok(dataStore.Inventory);

    [HttpPost]
    public IActionResult Create(InventoryRequest request)
    {
        var productExists = dataStore.Products.Any(product => product.Id == request.ProductId);
        var warehouseExists = dataStore.Warehouses.Any(warehouse => warehouse.Id == request.WarehouseId);

        if (!productExists || !warehouseExists)
        {
            return BadRequest(new { message = "ProductId and WarehouseId must reference valid records." });
        }

        var stock = new Inventory
        {
            ProductId = request.ProductId,
            WarehouseId = request.WarehouseId,
            QuantityOnHand = request.QuantityOnHand,
            ReservedQuantity = request.ReservedQuantity,
            ReorderLevel = request.ReorderLevel
        };

        dataStore.Inventory.Add(stock);
        var product = dataStore.Products.First(item => item.Id == request.ProductId);
        product.Quantity = Math.Max(product.Quantity, request.QuantityOnHand);
        return Created($"/inventory/{stock.Id}", stock);
    }

    [HttpPatch("{inventoryId:guid}/adjust")]
    public IActionResult Adjust(Guid inventoryId, InventoryAdjustmentRequest request)
    {
        var stock = dataStore.Inventory.FirstOrDefault(item => item.Id == inventoryId);
        if (stock is null)
        {
            return NotFound();
        }

        stock.Adjust(request.QuantityDelta);
        return Ok(stock);
    }
}
