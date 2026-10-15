using System;

namespace phoenix_warehouse.models
{
    public sealed class OrderItem
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal LineTotal => Quantity * UnitPrice;

        public void SetQuantity(int quantity)
        {
            if (quantity <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be positive.");
            }

            Quantity = quantity;
        }
    }
}