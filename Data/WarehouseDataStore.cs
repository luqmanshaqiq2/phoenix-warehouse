using phoenix_warehouse.models;

namespace phoenix_warehouse.Data;

public sealed class WarehouseDataStore
{
    public List<Product> Products { get; } =
    [
        new Product { SKU = "SKU-1001", ProductName = "Industrial Drill", Price = 129.99m, IsAvailable = true, Quantity = 45 },
        new Product { SKU = "SKU-1002", ProductName = "Hydraulic Jack", Price = 249.50m, IsAvailable = true, Quantity = 18 },
        new Product { SKU = "SKU-1003", ProductName = "Safety Gloves", Price = 24.99m, IsAvailable = true, Quantity = 120 }
    ];

    public List<Warehouse> Warehouses { get; } =
    [
        new Warehouse { Name = "North Hub", Location = "Chicago, IL", Capacity = 5000 },
        new Warehouse { Name = "South Hub", Location = "Dallas, TX", Capacity = 3500 }
    ];

    public List<Inventory> Inventory { get; }
    public List<Customer> Customers { get; } =
    [
        new Customer { FirstName = "Alicia", LastName = "Martinez", Email = "alicia.martinez@example.com", PhoneNumber = "555-0101" },
        new Customer { FirstName = "Marcus", LastName = "Chen", Email = "marcus.chen@example.com", PhoneNumber = "555-0102" }
    ];
    public List<Order> Orders { get; } = [];
    public List<StockMovement> StockMovements { get; } = [];
    public List<InventoryReservation> Reservations { get; } = [];

    public WarehouseDataStore()
    {
        Inventory =
        [
            new Inventory { ProductId = Products[0].Id, WarehouseId = Warehouses[0].Id, QuantityOnHand = 45, ReorderLevel = 10 },
            new Inventory { ProductId = Products[1].Id, WarehouseId = Warehouses[0].Id, QuantityOnHand = 18, ReorderLevel = 6 },
            new Inventory { ProductId = Products[2].Id, WarehouseId = Warehouses[1].Id, QuantityOnHand = 120, ReorderLevel = 25 }
        ];
    }
}
