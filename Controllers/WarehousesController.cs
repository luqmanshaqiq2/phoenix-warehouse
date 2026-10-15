using Microsoft.AspNetCore.Mvc;
using phoenix_warehouse.Data;
using phoenix_warehouse.models;

namespace phoenix_warehouse.Controllers;

[ApiController]
[Route("warehouses")]
public sealed class WarehousesController(WarehouseDataStore dataStore) : ControllerBase
{
    [HttpGet]
    public ActionResult<List<Warehouse>> GetAll() => Ok(dataStore.Warehouses);

    [HttpGet("{warehouseId:guid}")]
    public ActionResult<Warehouse> GetById(Guid warehouseId)
    {
        var warehouse = dataStore.Warehouses.FirstOrDefault(item => item.Id == warehouseId);
        return warehouse is null ? NotFound() : Ok(warehouse);
    }

    [HttpPost]
    public IActionResult Create(Warehouse warehouse)
    {
        if (string.IsNullOrWhiteSpace(warehouse.Name) || string.IsNullOrWhiteSpace(warehouse.Location))
        {
            return BadRequest(new { message = "Warehouse Name and Location are required." });
        }

        dataStore.Warehouses.Add(warehouse);
        return CreatedAtAction(nameof(GetById), new { warehouseId = warehouse.Id }, warehouse);
    }
}
