using Microsoft.EntityFrameworkCore;
using phoenix_warehouse.Data;
using phoenix_warehouse.Interface;
using phoenix_warehouse.models;

namespace phoenix_warehouse.Service;

public sealed class StockMovementService(WarehouseDbContext context) : IStockMovementService
{
    public Task<List<StockMovement>> GetAll() => context.StockMovements.ToListAsync();
}
