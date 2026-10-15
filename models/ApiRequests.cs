namespace phoenix_warehouse.models;

/// <summary>Inventory fields required to create a stock record.</summary>
public sealed record InventoryRequest(
    Guid ProductId,
    Guid WarehouseId,
    int QuantityOnHand,
    int ReservedQuantity,
    int ReorderLevel);

/// <summary>Quantity change applied to an inventory record.</summary>
public sealed record InventoryAdjustmentRequest(int QuantityDelta);

/// <summary>Customer and items required to create an order.</summary>
public sealed record CreateOrderRequest(Guid CustomerId, List<CreateOrderItemRequest> Items);

/// <summary>Product and quantity for a new order item.</summary>
public sealed record CreateOrderItemRequest(Guid ProductId, int Quantity);

/// <summary>New status for an existing order.</summary>
public sealed record OrderStatusUpdateRequest(string Status);
