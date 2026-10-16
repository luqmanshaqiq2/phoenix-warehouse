using phoenix_warehouse.models;

namespace phoenix_warehouse.Interface;

public interface IStockMovementService
{
    Task<List<StockMovement>> GetAll();
}
