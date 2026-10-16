using Microsoft.EntityFrameworkCore;
using phoenix_warehouse.Data;
using phoenix_warehouse.Interface;
using phoenix_warehouse.models;

namespace phoenix_warehouse.Service;

public sealed class OrderService(WarehouseDbContext context) : IOrderService
{
    public Task<List<Order>> GetAll() =>
        context.Orders.Include(order => order.Items).ToListAsync();

    public Task<Order?> GetById(Guid orderId) =>
        context.Orders.Include(order => order.Items).FirstOrDefaultAsync(order => order.Id == orderId);

    public async Task<Order> Create(CreateOrderRequest request)
    {
        var order = new Order { CustomerId = request.CustomerId, Status = "Pending" };

        foreach (var item in request.Items)
        {
            var product = await context.Products.FindAsync(item.ProductId);
            order.AddItem(product!, item.Quantity);
        }

        context.Orders.Add(order);
        await context.SaveChangesAsync();
        return order;
    }

    public async Task<Order?> UpdateStatus(Guid orderId, string status)
    {
        var order = await context.Orders.FindAsync(orderId);
        if (order is null)
        {
            return null;
        }

        order.UpdateStatus(status);
        await context.SaveChangesAsync();
        return order;
    }
}
