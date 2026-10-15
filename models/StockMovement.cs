using System;

namespace phoenix_warehouse.models
{
    public sealed class StockMovement
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public Guid ProductId { get; set; }
        public Guid WarehouseId { get; set; }
        public int Quantity { get; set; }
        public string MovementType { get; set; } = "Adjustment";
        public string Reason { get; set; } = string.Empty;
        public DateTime RecordedAt { get; private set; } = DateTime.UtcNow;

        public bool IsInbound => MovementType.Equals("Inbound", StringComparison.OrdinalIgnoreCase);
        public bool IsOutbound => MovementType.Equals("Outbound", StringComparison.OrdinalIgnoreCase);
    }
}