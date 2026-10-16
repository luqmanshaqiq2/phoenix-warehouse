using phoenix_warehouse.models;

namespace phoenix_warehouse.Interface;

public interface IOrderService
{
    Task<List<Order>> GetAll();
    Task<Order?> GetById(Guid orderId);
    Task<Order> Create(CreateOrderRequest request);
    Task<Order?> UpdateStatus(Guid orderId, string status);
}
