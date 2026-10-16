using Microsoft.EntityFrameworkCore;
using phoenix_warehouse.Data;
using phoenix_warehouse.Interface;
using phoenix_warehouse.models;

namespace phoenix_warehouse.Service;

public sealed class InventoryReservationService(WarehouseDbContext context) : IInventoryReservationService
{
    public Task<List<InventoryReservation>> GetAll() => context.InventoryReservations.ToListAsync();
}
