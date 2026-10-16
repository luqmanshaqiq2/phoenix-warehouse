using phoenix_warehouse.models;

namespace phoenix_warehouse.Interface;

public interface IWarehouseService
{
    Task<List<Warehouse>> GetAll();
    Task<Warehouse?> GetById(Guid warehouseId);
    Task<Warehouse> Create(Warehouse warehouse);
}
