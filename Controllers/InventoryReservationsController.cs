using Microsoft.AspNetCore.Mvc;
using phoenix_warehouse.Data;
using phoenix_warehouse.models;

namespace phoenix_warehouse.Controllers;

[ApiController]
[Route("inventory-reservations")]
public sealed class InventoryReservationsController(WarehouseDataStore dataStore) : ControllerBase
{
    [HttpGet]
    public ActionResult<List<InventoryReservation>> GetAll() => Ok(dataStore.Reservations);
}
