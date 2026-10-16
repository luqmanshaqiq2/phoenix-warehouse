using phoenix_warehouse.models;

namespace phoenix_warehouse.Interface;

public interface IInventoryService
{
    Task<List<Inventory>> GetAll();
    Task<Inventory> Create(InventoryRequest request);
    Task<Inventory?> Adjust(Guid inventoryId, int quantityDelta);
}
