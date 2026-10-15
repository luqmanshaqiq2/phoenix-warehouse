using Microsoft.EntityFrameworkCore;
using phoenix_warehouse.models;

namespace phoenix_warehouse.Data;

public sealed class WarehouseDbContext(DbContextOptions<WarehouseDbContext> options) : DbContext(options)
{
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Inventory> Inventory => Set<Inventory>();
    public DbSet<InventoryReservation> InventoryReservations => Set<InventoryReservation>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<StockMovement> StockMovements => Set<StockMovement>();
    public DbSet<Warehouse> Warehouses => Set<Warehouse>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>()
            .Property(order => order.TotalAmount)
            .HasPrecision(18, 2);
        modelBuilder.Entity<OrderItem>()
            .Property(item => item.UnitPrice)
            .HasPrecision(18, 2);
        modelBuilder.Entity<Product>()
            .Property(product => product.Price)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Inventory>()
            .HasOne<Product>()
            .WithMany()
            .HasForeignKey(inventory => inventory.ProductId);
        modelBuilder.Entity<Inventory>()
            .HasOne<Warehouse>()
            .WithMany(warehouse => warehouse.Inventory)
            .HasForeignKey(inventory => inventory.WarehouseId);

        modelBuilder.Entity<InventoryReservation>()
            .HasOne<Inventory>()
            .WithMany()
            .HasForeignKey(reservation => reservation.InventoryId);
        modelBuilder.Entity<InventoryReservation>()
            .HasOne<Product>()
            .WithMany()
            .HasForeignKey(reservation => reservation.ProductId)
            .OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<InventoryReservation>()
            .HasOne<Warehouse>()
            .WithMany()
            .HasForeignKey(reservation => reservation.WarehouseId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Order>()
            .HasOne<Customer>()
            .WithMany()
            .HasForeignKey(order => order.CustomerId);
        modelBuilder.Entity<Order>()
            .HasMany(order => order.Items)
            .WithOne()
            .HasForeignKey(item => item.OrderId);
        modelBuilder.Entity<OrderItem>()
            .HasOne<Product>()
            .WithMany()
            .HasForeignKey(item => item.ProductId);

        modelBuilder.Entity<StockMovement>()
            .HasOne<Product>()
            .WithMany()
            .HasForeignKey(movement => movement.ProductId);
        modelBuilder.Entity<StockMovement>()
            .HasOne<Warehouse>()
            .WithMany()
            .HasForeignKey(movement => movement.WarehouseId);

        modelBuilder.Entity<Inventory>().Ignore(inventory => inventory.AvailableQuantity);
        modelBuilder.Entity<InventoryReservation>().Ignore(reservation => reservation.IsActive);
        modelBuilder.Entity<OrderItem>().Ignore(item => item.LineTotal);
        modelBuilder.Entity<StockMovement>().Ignore(movement => movement.IsInbound);
        modelBuilder.Entity<StockMovement>().Ignore(movement => movement.IsOutbound);
    }
}
