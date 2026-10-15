using System;

namespace phoenix_warehouse.models
{
    public sealed class InventoryReservation
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public Guid InventoryId { get; set; }
        public Guid ProductId { get; set; }
        public Guid WarehouseId { get; set; }
        public int Quantity { get; set; }
        public DateTime ReservedAt { get; private set; } = DateTime.UtcNow;
        public DateTime? ExpiresAt { get; set; }
        public string Status { get; set; } = "Active";

        public bool IsActive => Status.Equals("Active", StringComparison.OrdinalIgnoreCase);

        public void Release()
        {
            Status = "Released";
        }
    }
}