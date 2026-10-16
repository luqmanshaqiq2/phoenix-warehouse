using Microsoft.EntityFrameworkCore;
using phoenix_warehouse.Data;
using phoenix_warehouse.Interface;
using phoenix_warehouse.models;

namespace phoenix_warehouse.Service;

public sealed class WarehouseService(WarehouseDbContext context) : IWarehouseService
{
    public Task<List<Warehouse>> GetAll() => context.Warehouses.ToListAsync();

    public Task<Warehouse?> GetById(Guid warehouseId) => context.Warehouses.FindAsync(warehouseId).AsTask();

    public async Task<Warehouse> Create(Warehouse warehouse)
    {
        context.Warehouses.Add(warehouse);
        await context.SaveChangesAsync();
        return warehouse;
    }
}
