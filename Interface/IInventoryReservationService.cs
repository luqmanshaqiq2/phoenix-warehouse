using phoenix_warehouse.models;

namespace phoenix_warehouse.Interface;

public interface IInventoryReservationService
{
    Task<List<InventoryReservation>> GetAll();
}
