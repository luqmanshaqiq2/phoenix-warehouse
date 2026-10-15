using System;

namespace phoenix_warehouse.models
{
    public sealed class Product
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string SKU { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; } = true;
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public DateTime UpdatedAt {get; private set; } = DateTime.UtcNow;

        public void UpdateQuantity(int quantity)
        {
            if (quantity < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity cannot be negative.");
            }

            Quantity = quantity;
            IsAvailable = quantity > 0;
        }
    }
}