using Microsoft.EntityFrameworkCore;
using phoenix_warehouse.Data;
using phoenix_warehouse.Interface;
using phoenix_warehouse.models;

namespace phoenix_warehouse.Service;

public sealed class InventoryService(WarehouseDbContext context) : IInventoryService
{
    public Task<List<Inventory>> GetAll() => context.Inventory.ToListAsync();

    public async Task<Inventory> Create(InventoryRequest request)
    {
        var inventory = new Inventory
        {
            ProductId = request.ProductId,
            WarehouseId = request.WarehouseId,
            QuantityOnHand = request.QuantityOnHand,
            ReservedQuantity = request.ReservedQuantity,
            ReorderLevel = request.ReorderLevel
        };

        context.Inventory.Add(inventory);
        var product = await context.Products.FindAsync(request.ProductId);
        product!.Quantity = Math.Max(product.Quantity, request.QuantityOnHand);
        await context.SaveChangesAsync();

        return inventory;
    }

    public async Task<Inventory?> Adjust(Guid inventoryId, int quantityDelta)
    {
        var inventory = await context.Inventory.FindAsync(inventoryId);
        if (inventory is null)
        {
            return null;
        }

        inventory.Adjust(quantityDelta);
        await context.SaveChangesAsync();
        return inventory;
    }
}
